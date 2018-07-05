using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMDateFrom_02
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
            return "LearnDelFAMDateFrom_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Ldfamfr02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLSF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateALB, DoMutateOptions = MutateGenerationOptionsALB },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLSF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLSF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateSOF, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateSOF(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
                });
            }
            else if (!valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateFrom = ld.LearnStartDate.AddDays(-1)
                });
            }

            ld.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateLSF(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }
            else if (!valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate.AddDays(-1),
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }

            ld.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateALB(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();
            ld.LearnAimRef = "6030599X";
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }
            else if (!valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate.AddDays(-1),
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }

            ld.LearningDeliveryFAM = ldfams.ToArray();
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
