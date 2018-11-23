using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_15
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
            return "EmpStat_15";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat15";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEmpStatus, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateEmpStatus(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            var empStatus = learner.LearnerEmploymentStatus;

                if (ld[0].FundModel == (int)FundModel.CommunityLearning)
                {
                    learner.DateOfBirth = ld[0].LearnStartDate.AddYears(-19);
                }

                if (!valid)
            {
                learner.LearnerEmploymentStatus[0].EmpStat = (int)EmploymentStatus.NoKnown;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
