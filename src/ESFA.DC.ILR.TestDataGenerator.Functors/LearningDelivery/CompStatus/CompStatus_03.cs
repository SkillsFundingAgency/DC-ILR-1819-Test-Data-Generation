﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class CompStatus_03
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
            return "CompStatus_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Compsts03";
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
                lds[0].CompStatus = (int)CompStatus.Completed;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.Achieved;
            }
        }

        private void MutateCompStatusWithdrawn(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            Mutate(learner, valid);
            if (!valid)
            {
                lds[0].CompStatus = (int)CompStatus.BreakInLearning;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.Partial;
            }
        }

        private void MutateCompStatusBreak(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            Mutate(learner, valid);
            if (!valid)
            {
                lds[0].CompStatus = (int)CompStatus.Withdrawn;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.Partial;
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
