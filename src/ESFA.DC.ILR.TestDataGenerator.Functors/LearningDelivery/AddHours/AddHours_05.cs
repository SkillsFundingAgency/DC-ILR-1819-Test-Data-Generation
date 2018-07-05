using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AddHours_05
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
            return "AddHours_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AddHr05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateAddHours, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateAddHoursLessThan60, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(13);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 71;
                }
            }
        }

        private void MutateAddHours(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(13);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 70;
                }
            }
        }

        private void MutateAddHoursLessThan60(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(6);
                    ld.AddHoursSpecified = true;
                    ld.AddHours = 36;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
