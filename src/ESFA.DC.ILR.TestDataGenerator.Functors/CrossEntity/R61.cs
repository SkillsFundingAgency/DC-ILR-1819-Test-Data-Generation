using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R61
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
            return "R61";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R61";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLSF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate,
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnStartDate.AddMonths(+9).AddDays(+3)
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                }
        }

        private void MutateLSF(MessageLearner learner, bool valid)
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
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate,
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnStartDate.AddMonths(+9).AddDays(+3)
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate.AddMonths(+9).AddDays(+1),
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnStartDate.AddMonths(+20).AddDays(+3)
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
