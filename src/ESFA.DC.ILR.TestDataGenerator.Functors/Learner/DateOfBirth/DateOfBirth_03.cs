using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_03
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate100AndADay, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "DOB_03";
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less100, Helpers.BasedOn.AYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Older);
        }

        private void Mutate100AndADay(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less100, Helpers.BasedOn.AYStart, Helpers.MakeOlderOrYoungerWhenInvalid.OlderTwo);
        }
    }
}
