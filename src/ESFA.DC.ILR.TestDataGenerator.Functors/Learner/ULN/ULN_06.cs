using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ULN_06
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.January;
        }

        public string RuleName()
        {
            return "ULN_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ULN_06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutatePlanEndDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutatePlanEndDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutatePlanEndDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutatePlanEndDateFM36, DoMutateOptions = MutateGenerationOptions, ValidLines = 1, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePlanEndDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutatePlanEndDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateCourseAndPlanEndDate, DoMutateOptions = MutateGenerationOptionsADL },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateActEndDate, DoMutateOptions = MutateGenerationOptionsCompleted },
            };
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            learner.ULN = 9999999999;
            learner.ULNSpecified = true;
        }

        private void MutateCourseAndPlanEndDate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString();
            MutatePlanEndDate(learner, valid);
        }

        private void MutatePlanEndDate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|DEC|13]"));
            if (valid)
            {
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(4);
            }
            else
            {
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(5);
            }

            MutateCommon(learner, valid);
        }

        private void MutatePlanEndDateFM36(MessageLearner learner, bool valid)
        {
            MutatePlanEndDate(learner, valid);
            learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate;
            learner.LearningDelivery[0].AppFinRecord[0].AFinDate = learner.LearningDelivery[1].LearnStartDate;
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMDateFrom = learner.LearningDelivery[1].LearnStartDate;
            learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(365);
        }

        private void MutateActEndDate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnActEndDateSpecified = true;
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Completed;
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|DEC|13]"));
            if (valid)
            {
                learner.LearningDelivery[0].LearnActEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(4);
            }
            else
            {
                learner.LearningDelivery[0].LearnActEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(5);
            }

            learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnActEndDate;
            MutateCommon(learner, valid);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsCompleted(GenerationOptions options)
        {
            options.LD.IncludeOutcome = true;
            options.CreateDestinationAndProgression = true;
        }

        private void MutateGenerationOptionsADL(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeADL = true;
            options.LD.IncludeHEFields = true;
            options.LD.IncludeSOF = true;
        }
    }
}
