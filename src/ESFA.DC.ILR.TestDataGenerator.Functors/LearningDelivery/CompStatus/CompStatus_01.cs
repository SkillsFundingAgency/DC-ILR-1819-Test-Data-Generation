using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class CompStatus_01
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
            return "CompStatus_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Compsts01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusCont, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusComp, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusWithdrawn, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusBreak, DoMutateOptions = MutateGenerationOptionsDestProg },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].CompStatusSpecified = true;
            Helpers.SetLearningDeliveryEndDates(lds[0], lds[0].LearnStartDate.AddDays(30), Helpers.SetAchDate.DoNotSetAchDate);
            lds[0].OutcomeSpecified = true;
        }

        private void MutateCompStatusCont(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].CompStatusSpecified = true;
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Continuing;
            if (!valid)
            {
                learner.LearningDelivery[0].CompStatus = 4;
            }
        }

        private void MutateCompStatusComp(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Completed;
            learner.LearningDelivery[0].Outcome = (int)Outcome.Achieved;
            if (!valid)
            {
                learner.LearningDelivery[0].CompStatus = 5;
            }
        }

        private void MutateCompStatusWithdrawn(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Withdrawn;
            learner.LearningDelivery[0].Outcome = (int)Outcome.Partial;
            learner.LearningDelivery[0].WithdrawReasonSpecified = true;
            learner.LearningDelivery[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;

            if (!valid)
            {
                learner.LearningDelivery[0].CompStatus = 8;
            }
        }

        private void MutateCompStatusBreak(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.BreakInLearning;
            learner.LearningDelivery[0].Outcome = (int)Outcome.Partial;
            if (!valid)
            {
                learner.LearningDelivery[0].CompStatus = 9;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            _options = options;
        }
    }
}
