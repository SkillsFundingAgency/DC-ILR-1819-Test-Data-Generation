using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AimSeqNumber_02
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
            return "AimSeqNumber_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Aimsq02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAS2, DoMutateOptions = MutateGenerationOptionsLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAS3, DoMutateOptions = MutateGenerationOptionsLD3 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAS1, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAS4, DoMutateOptions = MutateGenerationOptionsLD4, ExclusionRecord = true }
            };
        }

        private void MutateAS2(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[1].AimSeqNumber = 3;
            }
        }

        private void MutateAS3(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[2].AimSeqNumber = 5;
            }
        }

        private void MutateAS1(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].AimSeqNumber = 0;
            }
        }

        private void MutateAS4(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[2].AimSeqNumber = 4;
                learner.LearningDelivery[3].AimSeqNumber = 3;
            }
        }

        private void MutateGenerationOptionsLD3(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 3;
            _options = options;
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            _options = options;
        }

        private void MutateGenerationOptionsLD4(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 4;
            _options = options;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
