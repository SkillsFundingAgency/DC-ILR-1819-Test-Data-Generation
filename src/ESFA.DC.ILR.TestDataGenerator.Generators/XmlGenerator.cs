using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DCT.ILR.Model;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

namespace DCT.TestDataGenerator
{
    public class XmlGenerator
    {
        private RuleToFunctorParser _rfp;

        private List<XmlTriplet> _triplets;

        public XmlGenerator(RuleToFunctorParser rfp, int UKPRN)
        {
            _rfp = rfp;
            this.DefaultUKPRN = UKPRN;
            _triplets = new List<XmlTriplet>(100);
            _triplets.Add(new XmlTriplet() { UKPRN = DefaultUKPRN });
        }

        public int DefaultUKPRN { get; set; }

        public Message File { get; set; }

        public static void CreateAllFiles(RuleToFunctorParser rfp, List<ActiveRuleValidity> arv, int UKPRN, string folder, uint scale)
        {
            int ukprn = UKPRN;
            List<FileRuleLearner> allLearners = new List<FileRuleLearner>(1000);
            var filePrepsrequired = rfp.FilePreparationDateRequiredToRules(arv);
            if (filePrepsrequired[FilePreparationDateRequired.December].Count > 0 &&
                filePrepsrequired[FilePreparationDateRequired.January].Count > 0)
            {
                List<string> fpdrRules = new List<string>(filePrepsrequired[FilePreparationDateRequired.December].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.December]);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                XmlGenerator.CreateXMLFile(FilePreparationDateRequired.December, arv.Where(s => fpdrRules.Contains(s.RuleName)), rfp, ukprn, folder, scale, allLearners);
                fpdrRules.Clear();
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.January]);
                XmlGenerator.CreateXMLFile(FilePreparationDateRequired.January, arv, rfp, ukprn, folder, scale, allLearners);
            }
            else if (filePrepsrequired[FilePreparationDateRequired.December].Count > 0)
            {
                List<string> fpdrRules = new List<string>(filePrepsrequired[FilePreparationDateRequired.December].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.December]);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                XmlGenerator.CreateXMLFile(FilePreparationDateRequired.December, arv, rfp, ukprn, folder, scale, allLearners);
            }
            else
            {
                List<string> fpdrRules = new List<string>(filePrepsrequired[FilePreparationDateRequired.January].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.January]);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                XmlGenerator.CreateXMLFile(FilePreparationDateRequired.January, arv, rfp, ukprn, folder, scale, allLearners);
            }

            OutputControlFile(folder, allLearners);
        }

        public static void CreateXMLFile(FilePreparationDateRequired dateRequired, IEnumerable<ActiveRuleValidity> ruleNames, RuleToFunctorParser rfp, int UKPRN, string folder, uint scale, List<FileRuleLearner> allLearners)
        {
            XmlGenerator xml = new XmlGenerator(rfp, UKPRN);
            int learnerIndex = 0;
            long ULNIndex = 0;

            foreach (var arv in ruleNames)
            {
                for (uint i = 0; i != scale; ++i)
                {
                    learnerIndex += xml.GenerateRequiredLearnersAndProgression(arv.RuleName, arv.Valid, learnerIndex, ref ULNIndex);
                }
            }

            foreach (XmlTriplet trip in xml._triplets)
            {
                xml.PopulateHeaders(dateRequired, trip);
                xml.WriteFile(folder, (int)dateRequired, trip);
                allLearners.AddRange(trip.FileRuleLearners);
            }
        }

        public void WriteFile(string fileroot, int index, XmlTriplet triplet)
        {
            File.Learner = triplet.Learners.ToArray();
            File.LearnerDestinationandProgression = triplet.Progressions.ToArray();
            // ILR-LLLLLLLL-YYYY-yyyymmdd-hhmmss-NN.XML
            string filename = $"{fileroot}ILR-{triplet.UKPRN}-1718-{File.Header.Source.DateTime.ToString("yyyyMMdd-HHmmss")}-0{index}.xml";
            foreach (var frl in triplet.FileRuleLearners)
            {
                frl.Filename = filename;
            }

            System.Xml.Serialization.XmlSerializer writer =
                        new System.Xml.Serialization.XmlSerializer(typeof(Message));
            using (TextWriter sw = new StreamWriter(filename))
            {
                writer.Serialize(sw, File);
            }
        }

        internal void PopulateHeaders(FilePreparationDateRequired fpdr, XmlTriplet triplet)
        {
            File = new Message();
            File.Header = CreateHeader(fpdr, triplet.UKPRN);
            File.LearningProvider = CreateLearningProvider(triplet.UKPRN);
        }

        internal int GenerateRequiredLearnersAndProgression(string ruleName, bool valid, int currentLearnerIndex, ref long ULNIndex)
        {
            int result = _rfp.GenerateAndMutate(ruleName, valid, currentLearnerIndex, _triplets, ref ULNIndex);
            return result;
        }

        private static void OutputControlFile(string folder, List<FileRuleLearner> allLearners)
        {
            string filename = $"{folder}control-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.csv";
            using (TextWriter sw = new StreamWriter(filename))
            {
                var t = typeof(FileRuleLearner);
                var props = t.GetProperties();
                foreach (var prop in props)
                {
                    sw.Write(prop.Name + ",");
                }

                sw.WriteLine(string.Empty);
                foreach (var frl in allLearners)
                {
                    foreach (var prop in props)
                    {
                        sw.Write(prop.GetValue(frl) + ",");
                    }

                    sw.WriteLine(string.Empty);
                }
            }
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
                    Year = MessageHeaderCollectionDetailsYear.Item1718,
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
