using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_02
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
            return "EmpStat_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateTApprenticeship, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateTraineeship, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEmpStatus, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2014, 07, 30);
            Helpers.MutateApprenticeToTrainee(learner, _dataCache);
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HHS, LearnDelFAMCode.HHS_SingleWithChildren);
            MutateEmpStatus(learner, valid);
        }

        private void MutateTApprenticeship(MessageLearner learner, bool valid)
        {
            MutateEmpStatus(learner, valid);
        }

        private void MutateEmpStatus(MessageLearner learner, bool valid)
        {
            var empstat = learner.LearnerEmploymentStatus.ToList();
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

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
