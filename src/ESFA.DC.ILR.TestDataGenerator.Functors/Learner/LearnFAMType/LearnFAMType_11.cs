using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnFAMType_11
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        private Dictionary<LearnerFAMType, List<LearnerFAMCode>> _extraFAMs;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LFAM_11";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            var result = new List<LearnerTypeMutator>();
            _extraFAMs = new Dictionary<LearnerFAMType, List<LearnerFAMCode>>();
            _extraFAMs.Add(LearnerFAMType.NLM, new List<LearnerFAMCode>() { LearnerFAMCode.NLM_ContractLevel, LearnerFAMCode.NLM_Merger });
            _extraFAMs.Add(LearnerFAMType.EDF, new List<LearnerFAMCode>() { LearnerFAMCode.EDF_EnglishNotGot, LearnerFAMCode.EDF_MathsNotGot });
            _extraFAMs.Add(LearnerFAMType.PPE, new List<LearnerFAMCode>() { LearnerFAMCode.PPE_AdoptedCare, LearnerFAMCode.PPE_ServiceChild });
            foreach (var v in _extraFAMs)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var itt = _extraFAMs.GetEnumerator();
            itt.MoveNext();

            var kvp = itt.Current;
            foreach (LearnerFAMCode lfc in kvp.Value)
            {
                Helpers.AddLearnerFAM(learner, kvp.Key, lfc);
            }

            if (!valid)
            {
                Helpers.AddLearnerFAM(learner, kvp.Key, kvp.Value[0]);
            }

            _extraFAMs.Remove(kvp.Key);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
