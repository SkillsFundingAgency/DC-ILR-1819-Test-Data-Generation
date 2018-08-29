using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ProgType_07
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "ProgType_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PrgTyp07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate16Trainee, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void Mutate16Trainee(MessageLearner learner, bool valid)
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
                lds.LearnPlanEndDate = ld[0].LearnStartDate.AddMonths(6);
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
                    lde.LearnPlanEndDate = ld[0].LearnStartDate.AddMonths(6).AddDays(1);
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 3;
        }
    }
}
