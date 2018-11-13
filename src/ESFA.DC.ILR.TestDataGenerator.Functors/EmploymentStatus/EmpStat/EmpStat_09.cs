using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_09
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
            return "EmpStat_09";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat09";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLES, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTrainee, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateStartDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                var les = learner.LearnerEmploymentStatus[0];
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
            }

            if (!valid)
            {
                var les = learner.LearnerEmploymentStatus[0];
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(+2);
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var les = learner.LearnerEmploymentStatus[0];
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(+2);
            }
        }

        private void MutateStartDate(MessageLearner learner, bool valid)
        {
            MutateLES(learner, valid);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                led.LearnStartDate = new DateTime(2014, 07, 31);
            }
        }

        private void MutateTrainee(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
                learner.LearningDelivery[0].LearnAimRef = "ZPROG001";
                var les = learner.LearnerEmploymentStatus[0];
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(+2);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
