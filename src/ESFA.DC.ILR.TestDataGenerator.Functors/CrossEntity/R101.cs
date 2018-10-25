using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R101
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
            return "R101";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R101";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateACT, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateACT, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateValidACT, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateACT(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                var lds = learner.LearningDelivery[0];
                var fm = lds.FundModel;
                if (fm != 36)
                {
                    var ldfams = lds.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ACT.ToString());
                    lds.LearningDeliveryFAM = ldfams.ToArray();
                }
                else
                {
                    MutateValidACT(learner, valid);
                }
            }

            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                var ldfams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ACT.ToString());
                ld.LearningDeliveryFAM = ldfams.ToArray();
                        var lfams = ld.LearningDeliveryFAM.ToList();
                        lfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                            LearnDelFAMDateFrom = ld.LearnStartDate,
                            LearnDelFAMDateFromSpecified = true,
                            LearnDelFAMDateTo = ld.LearnStartDate.AddMonths(+8),
                            LearnDelFAMDateToSpecified = true
                        });
                        lfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                            LearnDelFAMDateFrom = ld.LearnStartDate.AddMonths(+7),
                            LearnDelFAMDateFromSpecified = true,
                            LearnDelFAMDateTo = ld.LearnPlanEndDate,
                            LearnDelFAMDateToSpecified = true
                        });
                        ld.LearningDeliveryFAM = lfams.ToArray();
            }
        }

        private void MutateValidACT(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                var ldfams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ACT.ToString());
                ld.LearningDeliveryFAM = ldfams.ToArray();
                var lfams = ld.LearningDeliveryFAM.ToList();
                lfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnStartDate.AddMonths(+7),
                    LearnDelFAMDateToSpecified = true
                });
                lfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate.AddMonths(+7).AddDays(+1),
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true,
                });
                ld.LearningDeliveryFAM = lfams.ToArray();
                ld.LearnActEndDate = ld.LearnPlanEndDate.AddMonths(-2);
                ld.LearnActEndDateSpecified = true;
            }

            //    ld.CompStatus = (int)CompStatus.BreakInLearning;
            //    ld.Outcome = (int)Outcome.NotYetKnown;
            //learner.LearningDelivery[1].LearnActEndDate = ld.LearnPlanEndDate;
            //learner.LearningDelivery[1].CompStatus = (int)CompStatus.BreakInLearning;
            //    learner.LearningDelivery[1].Outcome = (int)Outcome.NotYetKnown;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsALB(GenerationOptions options)
        {
            _options = options;
            _options.EmploymentRequired = true;
            _options.LD.IncludeADL = true;
        }
    }
}
