using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_64
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
            return "LearnDelFAMType_64";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy64";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateACTType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAIM3Type, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateACTType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            }
        }

        private void MutateAIM3Type(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].AimType = 3;
                learner.LearningDelivery[0].LearnAimRef = "50079189";
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
