namespace DCT.TestDataGenerator.Functor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DCT.ILR.Model;

    public class PrimaryLLDD_04
        : ILearnerMultiMutator
    {
        private List<LLDDHealthProb> _lldd;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PRILLDD_04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _lldd = new List<LLDDHealthProb>(30);
            var result = new List<LearnerTypeMutator>();
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLLDDCAT9899, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsStartDate });
            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            var l = learner.LLDDandHealthProblem.ToList();
            l.RemoveRange(1, 2);
            learner.LLDDandHealthProblem = l.ToArray(); ;
            foreach (var v in learner.LLDDandHealthProblem)
            {
                v.PrimaryLLDDSpecified = false;
            }
        }

        private void MutateLLDDCAT9899(MessageLearner learner, bool valid)
        {
            learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            int i = (int)LLDDCat.PreferNotToSay;
            foreach (var v in learner.LLDDandHealthProblem)
            {
                v.PrimaryLLDDSpecified = false;
                v.LLDDCat = i++;
            }

            learner.LLDDandHealthProblem = learner.LLDDandHealthProblem.Where(v => v.LLDDCat == (int)LLDDCat.NotProvided || v.LLDDCat == (int)LLDDCat.PreferNotToSay).ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
        }

        private void MutateGenerationOptionsStartDate(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2015-JUL-31");
        }
    }
}
