using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PostcodePrior_01
        : ILearnerMultiMutator
    {
        private readonly List<string> _nonExistPostcodes = new List<string> { "CV21 1UU", "CV21 1VA", "GY12 1SW" };
        private readonly List<string> _validPostcodes = new List<string> { "CV21 1RL", "CV1 1DX", "CV1 2AY" };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PostcodePrior_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PCodeP_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePostPriorOne, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePostPriorTwo, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.PostcodePrior = _validPostcodes[0];
            if (!valid)
            {
                learner.PostcodePrior = _nonExistPostcodes[0];
            }
        }

        private void MutatePostPriorOne(MessageLearner learner, bool valid)
        {
            learner.PostcodePrior = _validPostcodes[1];
            if (!valid)
            {
                learner.PostcodePrior = _nonExistPostcodes[1];
            }
        }

        private void MutatePostPriorTwo(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.PostcodePrior = _nonExistPostcodes[2];
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
