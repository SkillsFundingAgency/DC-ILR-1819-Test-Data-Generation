using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Postcode_14
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
            return "Postcode_14";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PCode_14";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePostOne, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePostTwo, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.Postcode = _validPostcodes[0];
            if (!valid)
            {
                learner.Postcode = _nonExistPostcodes[0];
            }
        }

        private void MutatePostOne(MessageLearner learner, bool valid)
        {
            learner.Postcode = _validPostcodes[1];
            if (!valid)
            {
                learner.Postcode = _nonExistPostcodes[1];
            }
        }

        private void MutatePostTwo(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.Postcode = _nonExistPostcodes[2];
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
