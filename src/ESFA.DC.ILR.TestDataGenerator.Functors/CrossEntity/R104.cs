using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R104
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
            return "R104";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R104";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateACT, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var lds = learner.LearningDelivery[0];
            var fm = lds.FundModel;
            if (valid)
            {
                if (fm != 36)
                {
                    var ldfams = lds.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ACT.ToString());
                    lds.LearningDeliveryFAM = ldfams.ToArray();
                }
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfam = ld.LearningDeliveryFAM.ToList();
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate,
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnStartDate.AddMonths(+9).AddDays(+3)
                    });
                    ld.LearningDeliveryFAM = ldfam.ToArray();
                    ld.LearnActEndDate = ld.LearnPlanEndDate.AddMonths(-2);
                    ld.LearnActEndDateSpecified = true;
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
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate,
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnStartDate.AddMonths(+9).AddDays(+3)
                    });
                    ldfam.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = learner.LearningDelivery[0].LearnStartDate.AddMonths(+9).AddDays(+1),
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = learner.LearningDelivery[0].LearnStartDate.AddMonths(+20).AddDays(+3)
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
