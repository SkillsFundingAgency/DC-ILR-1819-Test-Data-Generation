using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "FundModel_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FM_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsFullyFundedApprenticeships, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    ld.FundModel -= 2;
                }
            }
        }

        private void MutateAllComplete(MessageLearner learner, bool valid)
        {
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                Helpers.SetLearningDeliveryEndDates(ld, ld.LearnStartDate.AddDays(25), Helpers.SetAchDate.DoNotSetAchDate);
            }

            if (!valid)
            {
                learner.PlanLearnHoursSpecified = false;
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

        private void MutateGenerationOptionsFullyFundedApprenticeships(GenerationOptions options)
        {
            options.SetACTToFullyFunded = true;
        }
    }
}
