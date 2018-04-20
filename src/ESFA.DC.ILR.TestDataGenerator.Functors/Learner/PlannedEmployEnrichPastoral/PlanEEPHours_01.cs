using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PlanEEPHours_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PlanEEPHours_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PEEPH_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateAllComplete, DoMutateOptions = MutateGenerationOptionsProgression, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.PlanEEPHoursSpecified = true;
            learner.PlanEEPHours = 10;
            if (!valid)
            {
                learner.PlanEEPHoursSpecified = false;
            }
        }

        private void MutateAllComplete(MessageLearner learner, bool valid)
        {
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                Helpers.SetLearningDeliveryEndDates(ld, ld.LearnStartDate.AddDays(25), Helpers.SetAchDate.DoNotSetAchDate);
                if (ld.AimSeqNumber != 1)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }

            if (!valid)
            {
                learner.PlanEEPHoursSpecified = false;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutateGenerationOptionsProgression(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 3;
            options.CreateDestinationAndProgression = true;
        }
    }
}
