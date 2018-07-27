using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_56
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
            return "LearnAimRef_56";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR56";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLDM, DoMutateOptions = MutateGenerationOptionsCL, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLDM, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateOlass, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatRotl, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLDM(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = "034" // Parsing is removing leading zeros, will be corrected later
                });

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_RoTL).ToString()
                });

            ld.LearningDeliveryFAM = ldfams.ToArray();

            if (!valid)
            {
                    ld.LearnAimRef = "60148743";
            }
        }

        private void MutateOlass(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = "034"
                });
            ld.LearningDeliveryFAM = ldfams.ToArray();

            if (!valid)
            {
                    ld.LearnAimRef = "60148743";
            }
        }

        private void MutatRotl(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_RoTL).ToString()
                });
            ld.LearningDeliveryFAM = ldfams.ToArray();

            if (!valid)
            {
                    ld.LearnAimRef = "60148743";
            }
        }

        private void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();

            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = "034"
            });

            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_RoTL).ToString()
            });

            ld.LearningDeliveryFAM = ldfams.ToArray();

            if (!valid)
            {
                ld.LearnAimRef = "60148743";
                ld.LearnStartDate = new DateTime(2015, 08, 01).AddDays(-1);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsCL(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
