using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ULN_09
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.January;
        }

        public string RuleName()
        {
            return "ULN_09";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.ULNSpecified = true;
            if (!valid)
            {
                learner.ULN = 9999999999;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_OLASS;
        }
    }
}
