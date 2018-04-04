using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Ethnicity_01
        : ILearnerMultiMutator
    {
        private List<Ethnicity> _ethnicity;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "Eth_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _ethnicity = new List<Ethnicity>(30);
            var result = new List<LearnerTypeMutator>();
            foreach (var eth in Enum.GetValues(typeof(Ethnicity)))
            {
                _ethnicity.Add((Ethnicity)eth);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.Ethnicity = (int)_ethnicity[0];
            _ethnicity.RemoveAt(0);
            if (!valid)
            {
                learner.Ethnicity += 25;
                if (_ethnicity.Count == 0)
                {
                    learner.EthnicitySpecified = false;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
