using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_08
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
            return "LearnDelFAMType_08";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy08";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateSOFAdult, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateSOF1619, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateSOFOther, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateCommon(MessageLearner learner, bool valid, LearnDelFAMCode famcode)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-18);
            if (!valid)
            {
                Helpers.AddOrChangeLearningDeliverySourceOfFunding(learner.LearningDelivery[0], famcode);
            }
        }

        private void MutateSOFAdult(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, LearnDelFAMCode.SOF_ESFA_Adult);
        }

        private void MutateSOF1619(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, LearnDelFAMCode.SOF_ESFA_1619);
        }

        private void MutateSOFOther(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, LearnDelFAMCode.SOF_Other);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
