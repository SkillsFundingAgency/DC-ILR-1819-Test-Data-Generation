using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_57
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
            return "LearnAimRef_57";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR57";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateESM1619, DoMutateOptions = MutateGenerationOptionsCL, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateESM1619, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESM20Plus, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateESM20Plus, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLDMSteel, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMMandation, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2016, 07, 31).AddDays(-1);
                var ldfams = ld.LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_RoTL).ToString()
                });
                ld.LearningDeliveryFAM = ldfams.ToArray();
            }

            foreach (var les in learner.LearnerEmploymentStatus)
            {
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate;
                les.DateEmpStatAppSpecified = true;
            }
        }

        private void MutateCL(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
        }

        private void MutateESM1619(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

            lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
            {
                ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity1619,
                ESMCodeSpecified = true
            });

            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "50050916";
                }
            }
        }

        private void MutateESM20Plus(MessageLearner learner, bool valid)
        {
            var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

            lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
            {
                ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                ESMCodeSpecified = true
            });
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "50079013";
                    var ldFams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.LDM.ToString()).ToList();
                    ld.LearningDeliveryFAM = ldFams.ToArray();
                }
            }

            Mutate(learner, valid);
        }

        private void MutateLDMSteel(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

            lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
            {
                ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                ESMCodeSpecified = true
            });
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();

            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                var ldfams = ld.LearningDeliveryFAM.ToList();
                ld.LearnAimRef = "50079013";

                var ldFams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.LDM.ToString()).ToList();
                ld.LearningDeliveryFAM = ldFams.ToArray();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString()
                });

                ld.LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateLDMMandation(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

            lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
            {
                ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                ESMCodeSpecified = true
            });
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();

            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                var ldfams = ld.LearningDeliveryFAM.ToList();
                ld.LearnAimRef = "50079013";

                var ldFams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.LDM.ToString()).ToList();
                ld.LearningDeliveryFAM = ldFams.ToArray();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_MandationtoSkillsTraining).ToString()
                });

                ld.LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsCL(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
