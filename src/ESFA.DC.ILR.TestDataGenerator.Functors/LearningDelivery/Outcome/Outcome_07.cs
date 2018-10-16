using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Outcome_07
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
            return "Outcome_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutCom07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerNotAch, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerPartial, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression }
            };
        }

        private void MutateLearnerPartial(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            ld.OutcomeSpecified = true;
            ld.Outcome = (int)Outcome.Partial;
            MutateLearnerCommon(learner, valid);
        }

        private void MutateLearnerNotAch(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            ld.OutcomeSpecified = true;
            ld.Outcome = (int)Outcome.NoAchievement;
            MutateLearnerCommon(learner, valid);
        }

        private void MutateLearnerCommon(MessageLearner learner, bool valid)
        {
            // foreach (
            var ld = learner.LearningDelivery[0];
            ld.ProgTypeSpecified = true;
            ld.ProgType = (long)ProgType.Traineeship;
            ld.AimType = 1;
            ld.LearnAimRef = "ZPROG001";
            ld.LearnStartDate = new DateTime(2018, 04, 01);
            ld.LearnActEndDateSpecified = true;
            ld.LearnActEndDate = ld.LearnStartDate.AddMonths(5);
            ld.LearnPlanEndDate = ld.LearnStartDate.AddMonths(5);
            ld.CompStatus = (int)CompStatus.Completed;
           // }

            var empStat = learner.LearnerEmploymentStatus[0];
            empStat.EmploymentStatusMonitoring[0].ESMCode = 2;

            if (valid)
            {
                GenerationDestProgress(_options);
            }
        }

        private void GenerationDestProgress(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            _options = options;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
        }
    }
}
