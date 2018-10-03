using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R97
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
            return "R97";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R97";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateLES, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                List<MessageLearnerLearnerEmploymentStatus> empstat = learner.LearnerEmploymentStatus.ToList();
                empstat.Add(new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = (int)EmploymentStatus.PaidEmployment,
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
                empstat.Add(new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = (int)EmploymentStatus.PaidEmployment,
                    EmpStatSpecified = true,
                    DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1),
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
                learner.LearnerEmploymentStatus = empstat.Where(dt => dt.DateEmpStatApp != new DateTime(2017, 06, 10)).ToArray();
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                if (!valid)
                {
                    List<MessageLearnerLearnerEmploymentStatus> empstat = learner.LearnerEmploymentStatus.ToList();
                    empstat.Add(new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = (int)EmploymentStatus.PaidEmployment,
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
                    empstat.Add(new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = (int)EmploymentStatus.PaidEmployment,
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
                    learner.LearnerEmploymentStatus = empstat.Where(dt => dt.DateEmpStatApp != new DateTime(2017, 06, 10)).ToArray();
                }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }
    }
}
