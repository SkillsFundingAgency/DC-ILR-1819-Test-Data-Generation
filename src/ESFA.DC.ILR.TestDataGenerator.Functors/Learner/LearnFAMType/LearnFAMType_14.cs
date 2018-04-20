using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnFAMType_14
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LearnFAMType_14";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LFam_14";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            var result = new List<LearnerTypeMutator>();
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 });

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.AddLearnerFAM(learner, LearnerFAMType.SEN, LearnerFAMCode.SEN_Yes);
            if (!valid)
            {
                Helpers.AddLearnerFAM(learner, LearnerFAMType.EHC, LearnerFAMCode.EHC_Yes);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
