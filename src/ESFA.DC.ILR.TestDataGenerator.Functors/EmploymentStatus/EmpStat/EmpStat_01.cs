using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_01
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
            return "EmpStat_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateOLASS, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnCom, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLES, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTrainee, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                Mutate(learner, valid);
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var les = learner.LearnerEmploymentStatus[0];
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
            }
        }

            private void MutateTrainee(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateLearnCom(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            if (!valid) { Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.SOF, LearnDelFAMCode.SOF_LA); }
        }

        private void MutateOLASS(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            Mutate(learner, valid);
            if (!valid)
            {
                ld[0].LearningDeliveryFAM[0].LearnDelFAMType = LearnDelFAMType.LDM.ToString();
                ld[0].LearningDeliveryFAM[0].LearnDelFAMCode = "034";
            }

            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].LearnStartDate = new DateTime(2014, 07, 31);
            learner.DateOfBirth = ld[0].LearnStartDate.AddYears(-20);
            var empStatus = learner.LearnerEmploymentStatus;
            empStatus[0].DateEmpStatApp = ld[0].LearnStartDate;

            if (!valid)
            {
                empStatus[0].DateEmpStatApp = ld[0].LearnStartDate.AddDays(2);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
