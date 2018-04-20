namespace DCT.TestDataGenerator.Functor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DCT.ILR.Model;

    public class PrimaryLLDD_02
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PrimaryLLDD_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PLLDD_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            var result = new List<LearnerTypeMutator>();
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 });

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            learner.LLDDHealthProbSpecified = true;
            if (!valid)
            {
                var l = learner.LLDDandHealthProblem.ToList();
                l.ForEach(s => s.PrimaryLLDD += 2);
                learner.LLDDandHealthProblem = l.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
        }
    }
}
