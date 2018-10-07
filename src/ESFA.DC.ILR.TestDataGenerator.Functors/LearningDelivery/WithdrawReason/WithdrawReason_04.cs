using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class WithdrawReason_04
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
            return "WithdrawReason_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Wdrw04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusContinue, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusCompleted, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusBreakinLearn, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusWithdraw, DoMutateOptions = MutateGenerationOptionsDestProg, ExclusionRecord = true }
            };
        }

        private void MutateCompStatusContinue(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].CompStatus = (int)CompStatus.Continuing;
            ld[0].OutcomeSpecified = false;
            if (!valid)
            {
                ld[0].WithdrawReasonSpecified = true;
                ld[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;
            }
        }

        private void MutateCompStatusCompleted(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].CompStatus = (int)CompStatus.Completed;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.Partial;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddMonths(3);
            if (!valid)
            {
                ld[0].WithdrawReasonSpecified = true;
                ld[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;
            }
        }

        private void MutateCompStatusBreakinLearn(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].CompStatus = (int)CompStatus.BreakInLearning;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddMonths(3);
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.NoAchievement;
            if (!valid)
            {
                ld[0].WithdrawReasonSpecified = true;
                ld[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;
            }
        }

        private void MutateCompStatusWithdraw(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].CompStatus = (int)CompStatus.Withdrawn;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.Partial;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddMonths(3);
            ld[0].WithdrawReasonSpecified = true;
            ld[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
