using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class CompStatus_06
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
            return "CompStatus_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Compsts06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateOutcomePartialToAchieved, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateOutcomePartialToNotKnown, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateOutcomeNoAchievementToNotKnown, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateOutcomeNoAchievementToAchieved, DoMutateOptions = MutateGenerationOptionsDestProg },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].CompStatusSpecified = true;
            Helpers.SetLearningDeliveryEndDates(lds[0], lds[0].LearnStartDate.AddDays(30), Helpers.SetAchDate.DoNotSetAchDate);
            lds[0].OutcomeSpecified = true;
        }

        private void MutateOutcomePartialToAchieved(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Withdrawn;
            learner.LearningDelivery[0].Outcome = (int)Outcome.Partial;
            learner.LearningDelivery[0].WithdrawReasonSpecified = true;
            learner.LearningDelivery[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;

            if (!valid)
            {
                learner.LearningDelivery[0].Outcome = (int)Outcome.Achieved;
            }
        }

        private void MutateOutcomePartialToNotKnown(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Withdrawn;
            learner.LearningDelivery[0].Outcome = (int)Outcome.Partial;
            learner.LearningDelivery[0].WithdrawReasonSpecified = true;
            learner.LearningDelivery[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;

            if (!valid)
            {
                learner.LearningDelivery[0].Outcome = (int)Outcome.NotYetKnown;
            }
        }

        private void MutateOutcomeNoAchievementToNotKnown(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Withdrawn;
            learner.LearningDelivery[0].Outcome = (int)Outcome.NoAchievement;
            learner.LearningDelivery[0].WithdrawReasonSpecified = true;
            learner.LearningDelivery[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;

            if (!valid)
            {
                learner.LearningDelivery[0].Outcome = (int)Outcome.Achieved;
            }
        }

        private void MutateOutcomeNoAchievementToAchieved(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Withdrawn;
            learner.LearningDelivery[0].Outcome = (int)Outcome.NoAchievement;
            learner.LearningDelivery[0].WithdrawReasonSpecified = true;
            learner.LearningDelivery[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;

            if (!valid)
            {
                learner.LearningDelivery[0].Outcome = (int)Outcome.NotYetKnown;
            }
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
