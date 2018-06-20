using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnActEndDate_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLD2Restart, DoMutateOptions = MutateGenerationOptionsLD2, DoMutateProgression = Mutate19LD2RestartsDestAndProg }, // area uplift but with restarts
            };
        }

        public string RuleName()
        {
            return "LearnActEndDate_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LDEnd01";
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
        }

        private void MutateLD2Restart(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_ESFA_Adult, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            lds[0].LearnActEndDate = lds[0].LearnStartDate + TimeSpan.FromDays(45);
            lds[0].LearnPlanEndDate = lds[0].LearnStartDate + TimeSpan.FromDays(75);
            lds[0].LearnActEndDateSpecified = true;

            lds[0].CompStatus = (int)CompStatus.BreakInLearning;
            lds[0].Outcome = (int)Outcome.NoAchievement;
            lds[0].OutcomeSpecified = true;

            lds[1].LearnStartDate = lds[0].LearnActEndDate + TimeSpan.FromDays(30);
            Helpers.AddLearningDeliveryRestartFAM(lds[1]);
            lds[1].LearnPlanEndDate = lds[0].LearnPlanEndDate + TimeSpan.FromDays(45);
            lds[1].OrigLearnStartDate = lds[0].LearnStartDate;
            lds[1].OrigLearnStartDateSpecified = true;
            Helpers.SetLearningDeliveryEndDates(lds[1], lds[1].LearnPlanEndDate, Helpers.SetAchDate.DoNotSetAchDate);
            if (!valid)
            {
                Helpers.SetLearningDeliveryEndDates(lds[1], lds[1].LearnStartDate.AddDays(-1), Helpers.SetAchDate.DoNotSetAchDate);
            }

            lds[1].LearnAimRef = lds[0].LearnAimRef;

            _outcomeDate = lds[1].LearnPlanEndDate;
        }

        private void Mutate19LD2RestartsDestAndProg(MessageLearnerDestinationandProgression learner, bool valid)
        {
            learner.DPOutcome[0].OutStartDate = _outcomeDate;
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            options.LD.OverrideLearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|11]"));
            options.CreateDestinationAndProgression = true;
            options.EmploymentRequired = true;
            options.LD.IncludeADL = true;
            _options = options;
        }
    }
}
