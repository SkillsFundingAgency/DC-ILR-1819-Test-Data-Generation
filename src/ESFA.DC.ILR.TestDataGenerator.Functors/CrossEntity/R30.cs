using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R30
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
            return "R30";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R30";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            if (valid)
            {
                ld.AimTypeSpecified = true;
                ld.AimType = 4;
            }

            if (!valid)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 3;
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            if (!valid)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.ProgTypeSpecified = true;
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            if (valid)
            {
                ld.AimTypeSpecified = true;
                ld.AimType = 4;
            }

            if (!valid)
            {
                ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 3;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
