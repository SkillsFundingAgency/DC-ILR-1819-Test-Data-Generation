using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Postcode_15
        : ILearnerMultiMutator
    {
        private List<string> _invalidPostcode;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PCode_15";
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
