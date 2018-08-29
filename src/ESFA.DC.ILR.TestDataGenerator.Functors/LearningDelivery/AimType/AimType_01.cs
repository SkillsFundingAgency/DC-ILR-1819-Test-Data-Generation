using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AimType_01
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
            return "AimType_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AimTy01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAT13, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateAT4, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateAT5, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateAT13(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimTypeSpecified = true;
                    ld.AimType = 7;
                }
            }
        }

        private void MutateAT4(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.AimTypeSpecified = true;
                ld.AimType = 4;
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimTypeSpecified = true;
                    ld.AimType = 9;
                }
            }
        }

        private void MutateAT5(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.AimTypeSpecified = true;
                ld.AimType = 5;
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimTypeSpecified = true;
                    ld.AimType = 0;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
