using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ULN_05
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
                learner.ULN = 7673614579; // this is the "last" ULN in the LRS fountain table (as of 12-02-2018) so matches the ULN checksum but fails the "in list check"
                learner.ULNSpecified = true;
            }
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "ULN_05";
        }
    }
}
