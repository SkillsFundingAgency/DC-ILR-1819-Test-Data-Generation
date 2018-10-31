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
            return "Ethnicity_01";
        }

        public string LearnerReferenceNumberStub()
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
                if (learner.Ethnicity == 98 || learner.Ethnicity == 99)
                {
                    learner.Ethnicity -= 5;
                }
                else
                {
                    learner.Ethnicity += 25;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
