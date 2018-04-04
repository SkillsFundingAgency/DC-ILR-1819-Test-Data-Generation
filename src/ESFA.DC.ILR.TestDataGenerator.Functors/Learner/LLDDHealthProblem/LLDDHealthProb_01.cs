using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LLDDHealthProb_01
        : ILearnerMultiMutator
    {
        private List<LLDDHealthProb> _lldd;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LLDDHP_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _lldd = new List<LLDDHealthProb>(30);
            var result = new List<LearnerTypeMutator>();
            foreach (var eth in Enum.GetValues(typeof(LLDDHealthProb)))
            {
                _lldd.Add((LLDDHealthProb)eth);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LLDDHealthProb = (int)_lldd[0];
            _lldd.RemoveAt(0);
            if (learner.LLDDHealthProb == (int)LLDDHealthProb.NoLearningDifficultOrHealthProblem)
            {
                learner.LLDDandHealthProblem = null;
            }

            if (!valid)
            {
                learner.LLDDHealthProb += 3;
                if (_lldd.Count == 0)
                {
                    learner.LLDDHealthProbSpecified = false;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
        }
    }
}
