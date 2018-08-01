using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AimType_07
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
            return "AimType_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AimTy07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTraineeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateSOF(MessageLearner learner, bool valid)
        {
            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.SOF);
            learner.DateOfBirth = learner.DateOfBirth.AddYears(-2);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldfams = ld.LearningDeliveryFAM.ToList();
                ld.AimTypeSpecified = true;
                ld.AimType = 5;
                ld.LearnStartDate = new DateTime(2017, 08, 01).AddDays(-1);
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                });
                ld.LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.AimTypeSpecified = true;
                ld.AimType = 5;
                ld.LearnStartDate = new DateTime(2017, 08, 01).AddDays(-1);
            }
        }

        private void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            MutateSOF(learner, valid);

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDate = ld.LearnStartDate.AddDays(1);
                }
            }
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            MutateSOF(learner, valid);

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDate = ld.LearnStartDate.AddDays(1);
                    ld.ProgTypeSpecified = true;
                    ld.ProgType = (int)ProgType.Traineeship;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
