using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_07
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
            return "LearnDelFAMType_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateSOFAdult, DoMutateOptions = MutateGenerationOptionsProgression, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateSOF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateSOFAdult(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
            });

            var ldFams = learner.LearningDelivery[1].LearningDeliveryFAM.Where(s => s.LearnDelFAMCode != LearnDelFAMCode.SOF_ESFA_1619.ToString());
            learner.LearningDelivery[1].LearningDeliveryFAM = ldFams.ToArray();
            learner.LearningDelivery[1].AimType = (int)AimType.StandAlone;

            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.SOF);
            }
        }

        private void MutateSOF(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.SOF);
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsProgression(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            options.CreateDestinationAndProgression = true;
        }
    }
}
