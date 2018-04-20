using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ULN_04
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.December;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>() { new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions } };
        }

        public void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.ULN = 1234567890;
                learner.ULNSpecified = true;
            }
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "ULN_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ULN_04";
        }
    }
}
