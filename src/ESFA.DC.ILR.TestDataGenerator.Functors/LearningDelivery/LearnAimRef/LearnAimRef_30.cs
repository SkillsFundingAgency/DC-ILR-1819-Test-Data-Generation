using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_30
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
            return "LearnAimRef_30";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR30";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAimType, DoMutateOptions = MutateGenerationOptionsCL },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearnRef, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateAimType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.ComponentAim;
                }
            }
        }

        private void MutateLearnRef(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "ZESF0001";
                    ld.AimType = (int)AimType.ProgrammeAim;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }

        private void MutateGenerationOptionsCL(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
