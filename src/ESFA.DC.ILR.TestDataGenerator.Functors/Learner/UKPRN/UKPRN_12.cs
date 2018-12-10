using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class UKPRN_12
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
            return "UKPRN_12";
        }

        public string LearnerReferenceNumberStub()
        {
            return "UKPRN12";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptionsInvalid, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnActEndDate, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 11, 01).AddDays(1);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2017, 11, 01).AddDays(-1);
            }
        }

        private void MutateLearnActEndDate(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 11, 01).AddDays(1);
            learner.LearningDelivery[0].LearnActEndDateSpecified = true;
            learner.LearningDelivery[0].LearnActEndDate = new DateTime(2018, 08, 01).AddDays(-1);
        }

        private void MutateOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.AEBC).UKPRN;
        }

        private void MutateOptionsInvalid(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }
    }
}
