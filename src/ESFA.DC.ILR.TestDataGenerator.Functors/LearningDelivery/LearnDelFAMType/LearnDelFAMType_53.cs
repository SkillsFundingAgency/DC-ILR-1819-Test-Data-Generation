using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_53
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
            return "LearnDelFAMType_53";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy53";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateALB, DoMutateOptions = MutateOptionsInvalid },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldfam = ld.LearningDeliveryFAM.ToList();
                ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString(),
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate,
                    LearnDelFAMDateToSpecified = true,
                    LearnDelFAMDateTo = learner.LearningDelivery[0].LearnPlanEndDate
                });
                ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ADL.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ADL).ToString(),
                });
                ld.LearningDeliveryFAM = ldfam.ToArray();
            }
        }

        private void MutateALB(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate,
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnPlanEndDate
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ADL.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ADL).ToString(),
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                }
            }
        }

        private void MutateOptionsALLB(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.ALLB).UKPRN;
        }

        private void MutateOptionsInvalid(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
