using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PriorAttain_03
        : ILearnerMultiMutator
    {
        private List<PriorAttain> _attain;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PriorAttain_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PAtt_03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _attain = new List<PriorAttain>(30);
            var result = new List<LearnerTypeMutator>();
            foreach (var eth in Enum.GetValues(typeof(PriorAttain)))
            {
                _attain.Add((PriorAttain)eth);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.PriorAttain = (int)_attain[0];
            _attain.RemoveAt(0);
            if (!valid)
            {
                learner.PriorAttain += 13;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
