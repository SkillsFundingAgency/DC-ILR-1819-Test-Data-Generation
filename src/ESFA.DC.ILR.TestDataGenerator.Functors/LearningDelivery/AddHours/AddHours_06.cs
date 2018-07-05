using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AddHours_06
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
            return "AddHours_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AddHr06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateHighAvg, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateEqualLowAvg, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateEqualHighAvg, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(1);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 19;
                }
            }
        }

        private void MutateHighAvg(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(1);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 47;
                }
            }
        }

        private void MutateEqualLowAvg(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(1);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 18;
                }
            }
        }

        private void MutateEqualHighAvg(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(1);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 48;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
