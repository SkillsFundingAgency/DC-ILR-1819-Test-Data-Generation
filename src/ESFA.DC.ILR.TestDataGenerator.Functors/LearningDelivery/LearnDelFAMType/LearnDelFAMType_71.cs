using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_71
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
            return "LearnDelFAMType_71";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy71";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-25);
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_Pilot);
            if (valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.ACT, LearnDelFAMCode.ACT_ContractESFA);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
