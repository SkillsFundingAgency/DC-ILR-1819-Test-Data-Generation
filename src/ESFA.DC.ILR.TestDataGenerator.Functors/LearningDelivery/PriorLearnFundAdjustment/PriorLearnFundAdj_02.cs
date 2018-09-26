using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PriorLearnFundAdj_02
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PriorLearnFundAdj_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PlFndAdj02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAimType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateAimType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            Helpers.AddLearningDeliveryRestartFAM(learner);
            foreach (var lds in learner.LearningDelivery)
            {
                lds.PriorLearnFundAdjSpecified = true;
                lds.PriorLearnFundAdj = 99;
                lds.ProgTypeSpecified = true;
                lds.ProgType = (int)ProgType.Traineeship;
                lds.AimTypeSpecified = true;
                lds.AimType = (int)AimType.ComponentAim;
            }

            if (!valid)
            {
                foreach (var lde in learner.LearningDelivery)
                {
                    lde.PriorLearnFundAdjSpecified = false;
                }
            }
        }

        private void MutateAimType(MessageLearner learner, bool valid)
        {
            Helpers.AddLearningDeliveryRestartFAM(learner);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.PriorLearnFundAdjSpecified = true;
                ld.PriorLearnFundAdj = 99;
                ld.AimTypeSpecified = true;
                ld.AimType = (int)AimType.StandAlone;
            }

            if (!valid)
            {
                foreach (var lde in learner.LearningDelivery)
                {
                    lde.PriorLearnFundAdjSpecified = false;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsLD(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
        }
    }
}
