using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_66
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
            return "LearnDelFAMType_66";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy66";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateNVQLvl2_1, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateNVQLvl2_2, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateNVQLvl2_E, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateDD21, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateDD28, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMLowWages, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateEngMath, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDM347, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMOlass, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMRotl, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTraineeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateNVQLvl2_2, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-25);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 07, 31).AddDays(1);
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString(),
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateNVQLvl2_2(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60326001";
            }
        }

        private void MutateNVQLvl2_1(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60065564";
            }
        }

        private void MutateNVQLvl2_E(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60190115";
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            MutateNVQLvl2_2(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            MutateNVQLvl2_2(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60325999";
                learner.LearningDelivery[0].ProgType = (int)ProgType.Traineeship;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateLDMOlass(MessageLearner learner, bool valid)
        {
            MutateNVQLvl2_2(learner, valid);
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
            MutateNVQLvl2_2(learner, valid);
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
            MutateNVQLvl2_2(learner, valid);
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
            MutateNVQLvl2_2(learner, valid);
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
            MutateNVQLvl2_2(learner, valid);
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
            MutateNVQLvl2_2(learner, valid);
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
            MutateNVQLvl2_2(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60131676";
            }
        }

        private void MutateLDMLowWages(MessageLearner learner, bool valid)
        {
            MutateNVQLvl2_2(learner, valid);
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
