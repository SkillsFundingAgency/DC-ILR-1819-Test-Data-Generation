using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_65
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
            return "LearnDelFAMType_65";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy65";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateDD21, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateDD28, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESOL, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateEngMath, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDM347, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMOlass, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMRotl, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTraineeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDM363, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 07, 31).AddDays(1);
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString(),
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutatePriorAttain(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = (int)PriorAttain.Level2;
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60325999";
                learner.LearningDelivery[0].ProgType = (int)ProgType.Traineeship;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateLDMOlass(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = "034",
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateLDMRotl(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
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
            }
        }

        private void MutateDD28(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();
                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.BenefitEmploymentSupport,
                    ESMCodeSpecified = true
                });

                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
                foreach (var les in learner.LearnerEmploymentStatus)
                {
                    les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
                    les.DateEmpStatAppSpecified = true;
                    les.EmpStatSpecified = true;
                    les.EmpStat = 98;
                }
            }
        }

        private void MutateDD21(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();
                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.BenefitOther,
                    ESMCodeSpecified = true
                });

                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
                foreach (var les in learner.LearnerEmploymentStatus)
                {
                    les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
                    les.DateEmpStatAppSpecified = true;
                    les.EmpStatSpecified = true;
                    les.EmpStat = 11;
                }
            }
        }

        private void MutateRestarts(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateLDM347(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateEngMath(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60131676";
            }
        }

        private void MutateLDM363(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_LowWages).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
