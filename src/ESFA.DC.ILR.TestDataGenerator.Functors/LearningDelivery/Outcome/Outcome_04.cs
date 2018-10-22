using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Outcome_04
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
            return "Outcome_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutCom04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerNotComplete, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerNoDate, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression }
            };
        }

        private void MutateLearnerNotComplete(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                MutateCommon(ld);
                if (!valid)
                {
                    ld.Outcome = (int)Outcome.NoAchievement;
                }
            }
        }

        private void MutateLearnerNoDate(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                MutateCommon(ld);
                if (!valid)
                {
                    ld.AchDateSpecified = false;
                }
            }
        }

        private void MutateCommon(MessageLearnerLearningDelivery ld)
        {
            ld.ProgType = (long)ProgType.ApprenticeshipStandard;
            ld.LearnStartDate = new DateTime(2015, 07, 01);
            ld.AchDateSpecified = true;
            ld.AchDate = DateTime.Now;
            ld.OutcomeSpecified = true;
            ld.Outcome = (int)Outcome.Achieved;
            ld.LearnActEndDateSpecified = true;
            ld.LearnActEndDate = ld.LearnStartDate.AddMonths(9);
            ld.CompStatus = (int)CompStatus.Completed;
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dpo = learner.DPOutcome[0];
            dpo.OutCollDate = dpo.OutStartDate.AddDays(45);
        }
    }
}
