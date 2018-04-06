using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class MathGrade_02
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private Dictionary<string, string> _grade;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "Math_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _grade = new Dictionary<string, string>();
            foreach (var g in _dataCache.GCSEGrades())
            {
                char c = g[0];
                c += (char)8;
                while (g.Contains(c.ToString()))
                {
                    ++c;
                }

                _grade.Add(g, c.ToString());
            }

            var result = new List<LearnerTypeMutator>();
            foreach (var g in _grade)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.MathGrade = _grade.First().Key;
            if (!valid)
            {
                learner.MathGrade = _grade.First().Value;
            }

            _grade.Remove(_grade.First().Key);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.FAM.EFADisadvantageFundingRequired = true;
        }
    }
}
