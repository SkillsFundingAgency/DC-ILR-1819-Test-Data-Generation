using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_44
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
            return "LearnDelFAMType_44";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy44";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateHHSType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateHHSType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateHHSType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateAIM3Type, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateHHSType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.HHS);
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_OLASS).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateAIM3Type(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].AimType = 3;
                learner.LearningDelivery[0].LearnAimRef = "50079189";
            }
        }

        private void MutateAppType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].AimType = 3;
                learner.LearningDelivery[0].LearnAimRef = "50079189";
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = 25;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
