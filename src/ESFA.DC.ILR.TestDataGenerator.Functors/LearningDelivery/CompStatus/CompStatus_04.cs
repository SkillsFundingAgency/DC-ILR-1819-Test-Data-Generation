using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class CompStatus_04
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
            return "CompStatus_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Compsts04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusCont, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusComp, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusWithdrawn, DoMutateOptions = MutateGenerationOptionsDestProg },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusBreak, DoMutateOptions = MutateGenerationOptionsDestProg },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].CompStatusSpecified = true;
            lds[0].CompStatus = (int)CompStatus.Continuing;
        }

        private void MutateCompStatusCont(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
        }

        private void MutateCompStatusComp(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            Mutate(learner, valid);
            if (!valid)
            {
                lds[0].LearnPlanEndDate = lds[0].LearnStartDate.AddDays(30);
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(30);
                lds[0].CompStatus = (int)CompStatus.Completed;
            }
        }

        private void MutateCompStatusWithdrawn(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            Mutate(learner, valid);
            if (!valid)
            {
                lds[0].LearnPlanEndDate = lds[0].LearnStartDate.AddDays(30);
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(30);
                lds[0].CompStatus = (int)CompStatus.BreakInLearning;
            }
        }

        private void MutateCompStatusBreak(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            Mutate(learner, valid);
            if (!valid)
            {
                lds[0].LearnPlanEndDate = lds[0].LearnStartDate.AddDays(30);
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(30);
                lds[0].CompStatus = (int)CompStatus.Withdrawn;
                lds[0].WithdrawReasonSpecified = true;
                lds[0].WithdrawReason = (int)WithDrawalReason.OtherPersonalReasons;
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
