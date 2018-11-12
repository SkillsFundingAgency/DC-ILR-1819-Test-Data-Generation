using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ESMType_10
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
            return "ESMType_10";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ESMType10";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnerApp, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateLearnerApp(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                //AddEmpMonRec(learner, valid);
                learner.LearnerEmploymentStatus[0].EmpStat = (int)EmploymentStatus.LookingForWork;
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
                learner.LearnerEmploymentStatus[0].EmpStat = (int)EmploymentStatus.LookingForWork;
                if (valid)
                {
                    AddEmpMonRec(learner, valid);
                }
        }

        private void AddEmpMonRec(MessageLearner learner, bool valid)
        {
            var les = learner.LearnerEmploymentStatus[0];
            var lesm = les.EmploymentStatusMonitoring.ToList();
            lesm.RemoveRange(0, 2);
            lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
            {
                ESMType = EmploymentStatusMonitoringType.LOU.ToString(),
                ESMCode = (int)EmploymentStatusMonitoringCode.Unemployed611,
                ESMCodeSpecified = true
            });

            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
