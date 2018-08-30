using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ProgType_13
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
            return "ProgType_13";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PrgTyp13";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateActEndDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].LearnAimRef = "ZPROG001";
            ld[0].AimTypeSpecified = true;
            ld[0].AimType = (int)AimType.ProgrammeAim;
            ld[1].AimType = (int)AimType.ComponentAim;
            ld[2].AimType = (int)AimType.CoreAim1619;
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring[0].ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity16Less;
            foreach (var lds in learner.LearningDelivery)
            {
                lds.LearnStartDate = DateTime.Now.AddMonths(-8).AddDays(1);
                lds.LearnPlanEndDate = lds.LearnStartDate.AddMonths(6);
                lds.ProgTypeSpecified = true;
                lds.ProgType = (int)ProgType.Traineeship;
                lds.FworkCodeSpecified = false;
                lds.PwayCodeSpecified = false;
                lds.StdCodeSpecified = false;
            }

            if (!valid)
            {
                foreach (var lde in learner.LearningDelivery)
                {
                    lde.LearnStartDate = DateTime.Now.AddMonths(-8);
                    lde.LearnPlanEndDate = lde.LearnStartDate.AddMonths(6);
                }
            }
        }

        private void MutateActEndDate(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var lds in learner.LearningDelivery)
                {
                    lds.CompStatus = (int)CompStatus.Completed;
                    lds.LearnActEndDateSpecified = true;
                    lds.LearnActEndDate = lds.LearnStartDate.AddMonths(6);
                    lds.OutcomeSpecified = true;
                    lds.Outcome = (int)Outcome.NoAchievement;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 3;
        }
    }
}
