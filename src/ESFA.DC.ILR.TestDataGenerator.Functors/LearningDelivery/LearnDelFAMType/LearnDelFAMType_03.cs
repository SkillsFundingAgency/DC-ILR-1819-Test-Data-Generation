using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_03
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
            return "LearnDelFAMType_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLDM, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateASL, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var led = learner.LearningDelivery[0];
            var ldfams = led.LearningDeliveryFAM.ToList();
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
            });
            led.LearningDeliveryFAM = ldfams.ToArray();

            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    var ld0Fams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ASL.ToString());
                    ld.LearningDeliveryFAM = ld0Fams.ToArray();
                }
            }
        }

        private void MutateLDM(MessageLearner learner, bool valid)
        {
            var led = learner.LearningDelivery[0];
            var ldfams = led.LearningDeliveryFAM.ToList();
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
            });
            if (!valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_CommunityLearningMentalHealthPilot).ToString(),
                });
            }

            led.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateASL(MessageLearner learner, bool valid)
        {
            var led = learner.LearningDelivery[0];
            var ldfams = led.LearningDeliveryFAM.ToList();
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-18).AddMonths(-3);
            if (valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ASL.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ASL_Personal).ToString(),
                });
                led.LearningDeliveryFAM = ldfams.ToArray();
            }

            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    var ld0Fams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ASL.ToString());
                    ld.LearningDeliveryFAM = ld0Fams.ToArray();
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
