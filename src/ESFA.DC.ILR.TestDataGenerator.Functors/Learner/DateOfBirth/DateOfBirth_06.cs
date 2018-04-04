using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_06
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate13, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate13, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate13(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact13, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
