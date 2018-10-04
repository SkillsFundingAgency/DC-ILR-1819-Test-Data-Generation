using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_17
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
            return "EmpStat_17";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat17";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEmpStatus, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEmpStatusNull, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAimStartDate, DoMutateOptions = MutateGenerationOptionsEmpStatus, ExclusionRecord = true }
            };
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            learner.LearningDelivery[0].LearnStartDate = DateTime.Now.AddMonths(-3);
            Helpers.MutateApprenticeToTrainee(learner, _dataCache);
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HHS, LearnDelFAMCode.HHS_SingleWithChildren);
        }

        private void MutateEmpStatus(MessageLearner learner, bool valid)
        {
            var empstat = learner.LearnerEmploymentStatus.ToList();
            MutateTraineeship(learner, valid);
            if (!valid)
            {
                empstat.Add(new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = (int)EmploymentStatus.NoKnown,
                    EmpStatSpecified = true,
                    DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2),
                    DateEmpStatAppSpecified = true,
                    EmpId = 154549452,
                    EmpIdSpecified = true,
                    EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                    {
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                            ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity16Less,
                            ESMCodeSpecified = true
                        },
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMType = EmploymentStatusMonitoringType.LOE.ToString(),
                            ESMCode = (int)EmploymentStatusMonitoringCode.Employed12Plus,
                            ESMCodeSpecified = true
                        }
                    }
                });
                learner.LearnerEmploymentStatus = empstat.Where(es => es.EmpStat != 10).ToArray();
            }
        }

        // EmpStat Null is a field definition error
        private void MutateEmpStatusNull(MessageLearner learner, bool valid)
        {
            var empstat = learner.LearnerEmploymentStatus.ToList();
            MutateTraineeship(learner, valid);
            if (!valid)
            {
                empstat.Add(new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStatSpecified = false,
                    DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2),
                    DateEmpStatAppSpecified = true,
                    EmpId = 154549452,
                    EmpIdSpecified = true,
                    EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                    {
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                            ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                            ESMCodeSpecified = true
                        },
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMType = EmploymentStatusMonitoringType.LOE.ToString(),
                            ESMCode = (int)EmploymentStatusMonitoringCode.Employed12Plus,
                            ESMCodeSpecified = true
                        }
                    }
                });
                learner.LearnerEmploymentStatus = empstat.Where(es => es.EmpStat != 10).ToArray();
            }
        }

        private void MutateAimStartDate(MessageLearner learner, bool valid)
        {
            MutateEmpStatus(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDate = DateTime.Parse("2016-SEP-01").AddDays(-1);
                    ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(45);
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsEmpStatus(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
