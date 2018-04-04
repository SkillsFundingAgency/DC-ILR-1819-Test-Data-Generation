using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ALSCost_02 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public void Mutate(MessageLearner learner, bool valid)
        {
            learner.ALSCost = 1234;
            learner.ALSCostSpecified = true;
            if (!valid)
            {
                List<MessageLearnerLearnerFAM> fams = learner.LearnerFAM.Where(s => s.LearnFAMType != LearnerFAMType.HNS.ToString()).ToList();
                learner.LearnerFAM = fams.ToArray();
            }
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
            options.FAM.HighNeedsStudentRequired = true;
        }

        public string RuleName()
        {
            return "ALSCst_02";
        }
    }
}
