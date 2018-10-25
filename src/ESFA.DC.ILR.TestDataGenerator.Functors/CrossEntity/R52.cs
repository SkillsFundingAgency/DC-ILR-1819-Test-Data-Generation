using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R52
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
            return "R52";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R52";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsSOF },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateASF, DoMutateOptions = MutateGenerationOptionsSOF, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateACT, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateALB, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.HHS.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.HHS_Withheld).ToString()
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.HHS.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.HHS_Withheld).ToString()
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                }
            }
        }

        private void MutateASF(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString()
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString()
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                }
            }
        }

        private void MutateACT(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractESFA).ToString()
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractESFA).ToString()
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                }
            }
        }

        private void MutateALB(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString()
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString()
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
