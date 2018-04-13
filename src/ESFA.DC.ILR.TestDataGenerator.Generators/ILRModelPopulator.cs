using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DCT.ILR.Model;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

namespace DCT.TestDataGenerator
{
    public class ILRModelPopulator
    {
        private const string ESFA201819Namespace = "ESFA/ILR/2018-19";
        private const string SFA201718Namespace = "SFA/ILR/2017-18";
        private const string ESFA1819Year = "<Year>1819</Year>";
        private const string SFA1718Year = "<Year>1718</Year>";

        private RuleToFunctorParser _rfp;

        private List<XmlTriplet> _triplets;

        public ILRModelPopulator(RuleToFunctorParser rfp, int UKPRN)
        {
            _rfp = rfp;
            this.DefaultUKPRN = UKPRN;
        }

        public int DefaultUKPRN { get; set; }

        public Message File { get; set; }

        public Dictionary<string, string> PopulateXml(FilePreparationDateRequired dateRequired, IEnumerable<ActiveRuleValidity> ruleNames, uint scale, List<FileRuleLearner> allLearners, string ns)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(4);

            _triplets = new List<XmlTriplet>(100);
            _triplets.Add(new XmlTriplet() { UKPRN = DefaultUKPRN });

            int learnerIndex = 0;
            long ULNIndex = 0;

            foreach (var arv in ruleNames)
            {
                for (uint i = 0; i != scale; ++i)
                {
                    learnerIndex += GenerateRequiredLearnersAndProgression(arv.RuleName, arv.Valid, learnerIndex, ref ULNIndex);
                }
            }

            foreach (XmlTriplet trip in _triplets)
            {
                PopulateHeaders(dateRequired, trip);
                result.Add(Filename((int)dateRequired, trip), CreateXml((int)dateRequired, trip, ns));
                allLearners.AddRange(trip.FileRuleLearners);
            }

            return result;
        }

        private string Filename(int index, XmlTriplet triplet)
        {
            return $"ILR-{triplet.UKPRN}-1819-{File.Header.Source.DateTime.ToString("yyyyMMdd-HHmmss")}-0{index}.xml";
        }

        private string CreateXml(int index, XmlTriplet triplet, string ns)
        {
            File.Learner = triplet.Learners.ToArray();
            File.LearnerDestinationandProgression = triplet.Progressions.ToArray();
            // ILR-LLLLLLLL-YYYY-yyyymmdd-hhmmss-NN.XML
            string filename = Filename(index, triplet);
            foreach (var frl in triplet.FileRuleLearners)
            {
                frl.Filename = filename;
            }

            string result = string.Empty;
            System.Xml.Serialization.XmlSerializer writer =
                        new System.Xml.Serialization.XmlSerializer(typeof(Message));
            using (var stream = new MemoryStream(1024))
            {
                using (TextWriter sw = new StreamWriter(stream))
                {
                    writer.Serialize(sw, File);
                }

                result = Encoding.UTF8.GetString(stream.ToArray());
            }

            return result;
        }

        private void PopulateHeaders(FilePreparationDateRequired fpdr, XmlTriplet triplet)
        {
            File = new Message();
            File.Header = CreateHeader(fpdr, triplet.UKPRN);
            File.LearningProvider = CreateLearningProvider(triplet.UKPRN);
        }

        private int GenerateRequiredLearnersAndProgression(string ruleName, bool valid, int currentLearnerIndex, ref long ULNIndex)
        {
            int result = _rfp.GenerateAndMutate(ruleName, valid, currentLearnerIndex, _triplets, ref ULNIndex);
            return result;
        }

        private MessageLearningProvider CreateLearningProvider(int ukprn)
        {
            return new MessageLearningProvider()
            {
                UKPRN = ukprn
            };
        }

        private MessageHeader CreateHeader(FilePreparationDateRequired fpdr, int ukprn)
        {
            var result = new MessageHeader()
            {
                CollectionDetails = new MessageHeaderCollectionDetails()
                {
                    Collection = MessageHeaderCollectionDetailsCollection.ILR,
                    Year = MessageHeaderCollectionDetailsYear.Item1819,
                    FilePreparationDate = DateTime.Today //.ToString("yyyy-MM-dd")
                },
                Source = new MessageHeaderSource()
                {
                    DateTime = DateTime.Now,
                    ProtectiveMarking = MessageHeaderSourceProtectiveMarking.OFFICIALSENSITIVEPersonal,
                    Release = "0.1",
                    SoftwareSupplier = "Own Software",
                    UKPRN = ukprn,
                    SerialNo = "01"
                }
            };
            switch (fpdr)
            {
                case FilePreparationDateRequired.January:
                    result.CollectionDetails.FilePreparationDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|JAN|13]"));
                    break;
                case FilePreparationDateRequired.December:
                    result.CollectionDetails.FilePreparationDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|DEC|13]"));
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
