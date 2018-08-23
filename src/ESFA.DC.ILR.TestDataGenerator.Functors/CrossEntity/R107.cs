using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R107
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
            return "R107";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R107";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTrainee, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnActEndDateSpecified = true;
                learner.LearningDelivery[0].LearnActEndDate = new DateTime(2018, 07, 31);
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

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateTrainee(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60325999";
                learner.LearningDelivery[0].ProgType = (int)ProgType.Traineeship;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
        }
    }
}
