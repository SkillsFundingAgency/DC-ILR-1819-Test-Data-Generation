using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_67
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
            return "LearnDelFAMType_67";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy67";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[1].AimTypeSpecified = true;
                learner.LearningDelivery[1].AimType = 3;
                var ldfams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                });
                learner.LearningDelivery[1].LearningDeliveryFAM = ldfams.ToArray();
            }

            var ld = learner.LearningDelivery;
            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateOptions(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
        }
    }
}
