using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class MathGrade_03
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private List<string> _grade;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "Math_03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _grade = new List<string>();
            var result = new List<LearnerTypeMutator>();
            foreach (var g in _dataCache.GCSEDOrBelow())
            {
                _grade.Add(g);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.MathGrade = _grade.First();
            if (!valid)
            {
                Helpers.RemoveLearnerFAM(learner, LearnerFAMType.EDF);
            }

            _grade.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.FAM.EFADisadvantageFundingRequired = true;
        }
    }
}
