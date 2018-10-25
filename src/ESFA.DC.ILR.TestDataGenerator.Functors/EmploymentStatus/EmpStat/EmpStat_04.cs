using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_04
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "EmpStat_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLES, DoMutateOptions = MutatePLBGOptions }
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = (int)EmploymentStatus.PaidEmployment;
                learner.LearningDelivery[0].ConRefNumber = "ESF-2228";
                learner.LearningDelivery[1].ConRefNumber = "ESF-2228";
            }

            if (!valid)
            {
                learner.LearningDelivery[0].CompStatusSpecified = true;
                learner.LearningDelivery[0].CompStatus = (int)CompStatus.Completed;
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = (int)EmploymentStatus.NoKnown;
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate;
                learner.LearningDelivery[0].ConRefNumber = "ESF-2228";
                learner.LearningDelivery[1].ConRefNumber = "ESF-2228";
            }
        }

        private void MutateLESValid(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = (int)EmploymentStatus.PaidEmployment;
            //learner.LearningDelivery[0].ConRefNumber = "ESF-2228";
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutatePLBGOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }
    }
}
