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
        private const string ESFA201819Namespace = "ESFA/ILR/2018-19";
        private const string SFA201718Namespace = "SFA/ILR/2017-18";
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

        public IEnumerable<FileRuleLearner> CreateAllXml(List<ActiveRuleValidity> arv, uint scale, string ns)
        {
            List<FileRuleLearner> allLearners = new List<FileRuleLearner>(1000);
            var filePrepsrequired = _rfp.FilePreparationDateRequiredToRules(arv);

            if (filePrepsrequired[FilePreparationDateRequired.December].Count > 0 &&
                filePrepsrequired[FilePreparationDateRequired.January].Count > 0)
            {
                List<string> fpdrRules = new List<string>(filePrepsrequired[FilePreparationDateRequired.December].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.December]);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                CreateModelAndPopulateForRules(FilePreparationDateRequired.December, arv.Where(s => fpdrRules.Contains(s.RuleName)), scale, ns, allLearners);

                fpdrRules.Clear();
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.January]);
                CreateModelAndPopulateForRules(FilePreparationDateRequired.January, arv.Where(s => fpdrRules.Contains(s.RuleName)), scale, ns, allLearners);
            }
            else if (filePrepsrequired[FilePreparationDateRequired.December].Count > 0)
            {
                List<string> fpdrRules = new List<string>(filePrepsrequired[FilePreparationDateRequired.December].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.December]);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                CreateModelAndPopulateForRules(FilePreparationDateRequired.December, arv, scale, ns, allLearners);
            }
            else
            {
                List<string> fpdrRules = new List<string>(filePrepsrequired[FilePreparationDateRequired.January].Count + filePrepsrequired[FilePreparationDateRequired.None].Count);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.January]);
                fpdrRules.AddRange(filePrepsrequired[FilePreparationDateRequired.None]);
                CreateModelAndPopulateForRules(FilePreparationDateRequired.January, arv, scale, ns, allLearners);
            }

            return allLearners;
        }

        public Dictionary<string, string> FileContent()
        {
            return _fileContent;
        }

        private void CreateModelAndPopulateForRules(FilePreparationDateRequired filePreparationDateRequired,  IEnumerable<ActiveRuleValidity> arv, uint scale, string ns, List<FileRuleLearner> allLearners)
        {
            ILRModelPopulator model = new ILRModelPopulator(_rfp, DefaultUKPRN);
            foreach (var kvp in model.PopulateXml(filePreparationDateRequired, arv, scale, allLearners, ns))
            {
                _fileContent.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
