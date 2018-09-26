using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ProgType_14
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
            return "ProgType_14";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PrgTyp14";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, InvalidLines = 3 },
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
                lds.CompStatus = (int)CompStatus.Completed;
                lds.LearnPlanEndDate = lds.LearnStartDate.AddMonths(6);
                lds.LearnActEndDateSpecified = true;
                lds.LearnActEndDate = lds.LearnStartDate.AddMonths(6);
                lds.ProgTypeSpecified = true;
                lds.ProgType = (int)ProgType.Traineeship;
                lds.OutcomeSpecified = true;
                lds.Outcome = (int)Outcome.NoAchievement;
                lds.FworkCodeSpecified = false;
                lds.PwayCodeSpecified = false;
                lds.StdCodeSpecified = false;
            }

            if (!valid)
            {
                foreach (var lde in learner.LearningDelivery)
                {
                    lde.LearnAimRef = "ZWRKX002";
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 3;
            options.CreateDestinationAndProgression = true;
        }
    }
}
