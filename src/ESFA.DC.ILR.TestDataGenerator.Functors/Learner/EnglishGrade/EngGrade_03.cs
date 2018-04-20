using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EngGrade_03
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private List<string> _grade;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
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

        public string RuleName()
        {
            return "EngGrade_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Eng_03";
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.EngGrade = _grade.First();
            Helpers.AddOrChangeLearnerFAM(learner, LearnerFAMType.EDF, LearnerFAMCode.EDF_EnglishNotGot);
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
