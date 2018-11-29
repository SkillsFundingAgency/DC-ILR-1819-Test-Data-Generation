using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnStartDate_15
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "LearnStartDate_15";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LstartDt15";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFFIFAM(learner);
                learner.LearningDelivery[0].CompStatusSpecified = true;
                learner.LearningDelivery[0].CompStatus = 2;
                learner.LearningDelivery[0].LearnActEndDate = DateTime.Now.AddDays(-2);
                learner.LearningDelivery[0].LearnActEndDateSpecified = true;
                learner.LearningDelivery[0].OutcomeSpecified = true;
                learner.LearningDelivery[0].Outcome = (int)Outcome.NotYetKnown;
                learner.LearningDelivery[1].LearnStartDateSpecified = true;
                learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
        }
    }
}
