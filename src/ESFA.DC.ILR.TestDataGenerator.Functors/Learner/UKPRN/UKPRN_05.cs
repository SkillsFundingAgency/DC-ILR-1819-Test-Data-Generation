using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class UKPRN_05
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
            return "UKPRN_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "UKPRN05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLES, DoMutateOptions = MutatePLBGOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLearnActEndDate, DoMutateOptions = MutatePLBGOptions, ExclusionRecord = true }
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
                learner.LearningDelivery[0].ConRefNumber = "ALLB-4051";
            }
        }

        private void MutateLearnActEndDate(MessageLearner learner, bool valid)
        {
            MutateLES(learner, valid);

            if (!valid)
            {
                learner.LearningDelivery[0].LearnActEndDate = new DateTime(2018, 07, 30);
            }
        }

        private void MutatePLBGOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }
    }
}
