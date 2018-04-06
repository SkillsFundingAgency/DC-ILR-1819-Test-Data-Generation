using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LLDDCat_02
        : ILearnerMultiMutator
    {
        private List<LLDDCatValidity> _lldd;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LLDDC_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _lldd = cache.LLDDCatValidity().ToList();
            var result = new List<LearnerTypeMutator>();

            foreach (var eth in _lldd)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsStartDate });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LLDDandHealthProblem[0].LLDDCat = (int)_lldd[0].Category;
            learner.LLDDandHealthProblem[0].LLDDCatSpecified = true;
            learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = _lldd[0].To;
                if (!valid)
                {
                    ld.LearnStartDate = ld.LearnStartDate.AddDays(1);
                }
            }

            _lldd.RemoveAt(0);
        }

        private void MutateGenerationOptionsStartDate(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
            options.LD.OverrideLearnStartDate = _lldd[0].To; //  DateTime.Parse("2015-JUL-31");
        }
    }
}
