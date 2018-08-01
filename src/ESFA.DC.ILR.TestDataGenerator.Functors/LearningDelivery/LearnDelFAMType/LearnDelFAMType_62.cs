using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_62
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
            return "LearnDelFAMType_62";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy62";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateFFI, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTraineeType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRoTLType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESMTypeOne, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESMTypeTwo, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESMTypeThree, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateBasicskills, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRES, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESOL, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateSteel, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateFFI(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
                learner.LearningDelivery[0].ProgType = 23;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateTraineeType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60325999";
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_OLASS).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateRoTLType(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_RoTL).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateESMTypeOne(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.BenefitEmploymentSupport,
                    ESMCodeSpecified = true
                });
                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
            }
        }

        private void MutateESMTypeTwo(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                learner.LearnerEmploymentStatus[0].EmpStatSpecified = true;
                learner.LearnerEmploymentStatus[0].EmpStat = 11;
                var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.BenefitOther,
                    ESMCodeSpecified = true
                });
                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
            }
        }

        private void MutateESMTypeThree(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();
                lesm.RemoveRange(0, 1);
                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity16Less,
                    ESMCodeSpecified = true,
                });
                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.BenefitOther,
                    ESMCodeSpecified = true
                });
                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
            }
        }

        private void MutateBasicskills(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2016, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60315659";
            }
        }

        private void MutateRES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2016, 12, 01);
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60315659";
            }
        }

        private void MutateESOL(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60301053";
            }
        }

        private void MutateSteel(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60315659";
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
