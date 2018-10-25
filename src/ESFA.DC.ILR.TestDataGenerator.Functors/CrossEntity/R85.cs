using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R85
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
            return "R85";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R85";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionRef },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].CompStatusSpecified = true;
                learner.LearningDelivery[0].CompStatus = 3;
                learner.LearningDelivery[0].OutcomeSpecified = true;
                learner.LearningDelivery[0].Outcome = 3;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            if (!valid)
            {
                learner.ULN = 9900000001;
            }
        }

        private void MutateProgressionRef(MessageLearnerDestinationandProgression learner, bool valid)
        {
            if (!valid)
            {
                learner.LearnRefNumber = "10R85";
            }
        }
    }
}
