using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PostcodePrior_02
        : ILearnerMultiMutator
    {
        private List<string> _invalidPostcode;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PostcodePrior_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PCodeP_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _invalidPostcode = cache.InvalidPostcode().ToList();
            var result = new List<LearnerTypeMutator>();
            for (int i = 0; i != _invalidPostcode.Count; ++i)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.PostcodePrior = "AA1A 0AA";
            if (!valid)
            {
                learner.PostcodePrior = _invalidPostcode[0];
                _invalidPostcode.RemoveAt(0);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
