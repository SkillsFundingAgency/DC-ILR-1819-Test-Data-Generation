using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_04
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ValidLines = 1, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate115AndADay, DoMutateOptions = MutateGenerationOptions, ValidLines = 1, InvalidLines = 2 }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less115, Helpers.BasedOn.AYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Older);
        }

        private void Mutate115AndADay(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less115, Helpers.BasedOn.AYStart, Helpers.MakeOlderOrYoungerWhenInvalid.OlderTwo);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
