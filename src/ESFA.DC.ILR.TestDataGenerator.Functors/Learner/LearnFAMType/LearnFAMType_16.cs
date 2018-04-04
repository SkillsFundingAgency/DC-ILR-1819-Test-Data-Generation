using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnFAMType_16
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private Dictionary<LearnerFAMType, LearnerFAMCode> _extraFAMs;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LFAM_16";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _extraFAMs = new Dictionary<LearnerFAMType, LearnerFAMCode>();
            _extraFAMs.Add(LearnerFAMType.SEN, LearnerFAMCode.SEN_Yes);
            _extraFAMs.Add(LearnerFAMType.EHC, LearnerFAMCode.EHC_Yes);

            var result = new List<LearnerTypeMutator>();
            foreach (var v in _extraFAMs)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.AddLearnerFAM(learner, LearnerFAMType.ECF, LearnerFAMCode.ECF_ExcemptLearningDifficulty);
            var itt = _extraFAMs.GetEnumerator();
            itt.MoveNext();

            var kvp = itt.Current;
            if (valid)
            {
                Helpers.AddLearnerFAM(learner, kvp.Key, kvp.Value);
            }

            _extraFAMs.Remove(kvp.Key);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
