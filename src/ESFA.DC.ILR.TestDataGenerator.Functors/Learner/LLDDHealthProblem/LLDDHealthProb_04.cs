using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LLDDHealthProb_04
        : ILearnerMultiMutator
    {
        private List<LLDDHealthProb> _lldd;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LLDDHealthProb_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LLDDHP_04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _lldd = new List<LLDDHealthProb>(30);
            var result = new List<LearnerTypeMutator>() {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
        }
    }
}
