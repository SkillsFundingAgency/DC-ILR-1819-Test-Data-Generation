using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpOutcome_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "EmpOutcome_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpOut_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsProgression, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateDOB, DoMutateOptions = MutateGenerationOptionsProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateDOB, DoMutateOptions = MutateGenerationOptionsSOF },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    ld.EmpOutcomeSpecified = true;
                    ld.EmpOutcome = 1;
                }
            }
        }

        private void MutateDOB(MessageLearner learner, bool valid)
        {
           var learnerTypeRequired = learner.LearningDelivery[0].FundModel;
            switch (learnerTypeRequired)
            {
                case 99:
                    learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(1);
                    break;

                case 10:
                    learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                    break;
            }

           Mutate(learner, valid);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsProgression(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
