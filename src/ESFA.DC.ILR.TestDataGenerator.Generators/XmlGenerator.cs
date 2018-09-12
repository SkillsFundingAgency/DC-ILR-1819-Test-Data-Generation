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
    public class XmlGenerator
    {
        public const string ESFA201819Namespace = "ESFA/ILR/2018-19";
        public const string SFA201718Namespace = "SFA/ILR/2017-18";
        private const string ESFA1819Year = "<Year>1819</Year>";
        private const string SFA1718Year = "<Year>1718</Year>";

        private RuleToFunctorParser _rfp;
        private Dictionary<string, string> _fileContent;

        public XmlGenerator(RuleToFunctorParser rfp, int UKPRN)
        {
            _rfp = rfp;
            this.DefaultUKPRN = UKPRN;
            _fileContent = new Dictionary<string, string>(4);
        }

        public int DefaultUKPRN { get; set; }

        public Message File { get; set; }

        public IEnumerable<FileRuleLearner> CreateAllXml(List<ActiveRuleValidity> arv, int scale, string ns)
        {
            List<FileRuleLearner> allLearners = new List<FileRuleLearner>(1000);
            var filePrepsrequired = _rfp.FilePreparationDateRequiredToRules(arv);

            bool noSpecificDateProcessed = filePrepsrequired[FilePreparationDateRequired.None].Count == 0;

            List<FilePreparationDateRequired> filesByDate = new List<FilePreparationDateRequired>()
            {
                FilePreparationDateRequired.January,
                FilePreparationDateRequired.December,
                FilePreparationDateRequired.July,
                FilePreparationDateRequired.Future
            };

            foreach (var fbd in filesByDate)
            {
                if (filePrepsrequired[fbd].Count > 0)
                {
                    List<string> fpdrRules = new List<string>(filePrepsrequired[fbd].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                    fpdrRules.AddRange(filePrepsrequired[fbd]);
                    if (!noSpecificDateProcessed)
                    {
                        noSpecificDateProcessed = true;
                        fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                    }

                    CreateModelAndPopulateForRules(fbd, arv.Where(s => fpdrRules.Contains(s.RuleName)), scale, ns, allLearners);
                }
            }

            if (!noSpecificDateProcessed)
            {
                var fbd = FilePreparationDateRequired.None;
                List<string> fpdrRules = new List<string>(filePrepsrequired[fbd].Count);
                fpdrRules.AddRange(filePrepsrequired[fbd]);
                CreateModelAndPopulateForRules(FilePreparationDateRequired.January, arv.Where(s => fpdrRules.Contains(s.RuleName)), scale, ns, allLearners);
            }

            return allLearners;
        }

        public Dictionary<string, string> FileContent()
        {
            return _fileContent;
        }

        private void CreateModelAndPopulateForRules(FilePreparationDateRequired filePreparationDateRequired,  IEnumerable<ActiveRuleValidity> arv, int scale, string ns, List<FileRuleLearner> allLearners)
        {
            ILRModelPopulator model = new ILRModelPopulator(_rfp, DefaultUKPRN);
            foreach (var kvp in model.PopulateXml(filePreparationDateRequired, arv, scale, allLearners, ns))
            {
                _fileContent.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
