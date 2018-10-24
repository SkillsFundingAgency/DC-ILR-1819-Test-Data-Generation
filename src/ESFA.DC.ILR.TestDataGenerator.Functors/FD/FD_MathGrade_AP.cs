using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FD_MathGrade_AP
        : ILearnerMultiMutator
    {
        List<string> valid = new List<string> { "D", "DD", "DE", "E", "EE", "EF", "F", "FF", "FG", "G", "GG", "N", "U" };
        List<string> invalid = new List<string>
        { "A*", "A", "B", "C", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        private ILearnerCreatorDataCache _dataCache;
        private Dictionary<string, string> _grade;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "FD_MathGrade_AP";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FDMath";
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
