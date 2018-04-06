using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_43
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_43";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact15, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
            Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(30), Helpers.SetAchDate.DoNotSetAchDate);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.AimDefaultType = GenerationOptions.AimTypes.ESFValidAims;
        }
    }
}
