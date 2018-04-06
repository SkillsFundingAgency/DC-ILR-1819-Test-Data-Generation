using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnFAMType_10
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LFAM_10";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            var result = new List<LearnerTypeMutator>();
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.AddLearnerFAM(learner, LearnerFAMType.LSR, LearnerFAMCode.LSR_ResidentialAccess);
            Helpers.AddLearnerFAM(learner, LearnerFAMType.LSR, LearnerFAMCode.LSR_Hardship);
            Helpers.AddLearnerFAM(learner, LearnerFAMType.LSR, LearnerFAMCode.LSR_Discretionary);
            if (!valid)
            {
                Helpers.AddLearnerFAM(learner, LearnerFAMType.LSR, LearnerFAMCode.LSR_Childcare);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.FAM.LearnerSupportRequired = true;
        }
    }
}
