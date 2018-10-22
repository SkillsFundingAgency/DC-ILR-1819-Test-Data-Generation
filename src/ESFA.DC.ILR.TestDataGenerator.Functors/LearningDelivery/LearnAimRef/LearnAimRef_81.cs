using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_81
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
            return "LearnAimRef_81";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR81";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateExclusion, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
                };
        }

        private void MutateExclusion(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            ld.LearningDeliveryFAM[0].LearnDelFAMType = LearnDelFAMType.LDM.ToString();
            ld.LearningDeliveryFAM[0].LearnDelFAMCode = "347";
            MutateLearner(learner, valid);
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2016, 08, 01);
                ld.LearnAimRef = !valid ? "60110016" : "60027769";   // LARS_LearningDeliveryCategory.CategoryRef=20 and all child records of category 20
            }

            var empStat = learner.LearnerEmploymentStatus[0];
            empStat.EmpStat = 10;
            empStat.EmploymentStatusMonitoring[0].ESMCode = 3;
            empStat.EmploymentStatusMonitoring[0].ESMType = "BSI";
            empStat.DateEmpStatApp = new DateTime(2016, 08, 01);
            AddEmpStatMonitor(learner);
            //RemoveFAM(learner);
        }

            private void RemoveFAM(MessageLearner learner)
            {
                var ldel = learner.LearningDelivery.ToList();
                foreach (var ld in ldel)
                {
                    var ldFAMs = ld.LearningDeliveryFAM.ToList();
                    foreach (var fam in ldFAMs)
                    {
                       if (fam.LearnDelFAMType == "LDM")
                        {
                            ldFAMs.Remove(fam);
                            ld.LearningDeliveryFAM = ldFAMs.ToArray();
                            break;
                        }
                    }

                    learner.LearningDelivery = ldel.ToArray();
                }
            }

        private void AddEmpStatMonitor(MessageLearner learner)
        {
            var empstat = learner.LearnerEmploymentStatus.ToList();
            var empStatusMonitoring = empstat[0].EmploymentStatusMonitoring.ToList();
            MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring newStatusmonitor = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring {
                ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                ESMCode = (long)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                ESMCodeSpecified = true
            };
            empStatusMonitoring.Add(newStatusmonitor);
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = empStatusMonitoring.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
