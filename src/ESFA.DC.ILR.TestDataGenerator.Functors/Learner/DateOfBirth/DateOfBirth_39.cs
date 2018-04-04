using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_39
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_39";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
        //    AdvancedLevelApprenticeship = 2,
        //IntermediateLevelApprenticeship = 3,
        //HigherApprenticeshipLevel4 = 20,
        //HigherApprenticeshipLevel5 = 21,
        //HigherApprenticeshipLevel6 = 22,
        //HigherApprenticeshipLevel7 = 23,
        //Traineeship = 24,
        //ApprenticeshipStandard = 25
            return new List<LearnerTypeMutator>()
            {
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19FM81, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19IntermediateLevelApprenticeshipFM81, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship4FM81, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 6 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship6, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 6 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship6Restart, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true, InvalidLines = 4 }
            };
        }

        private void Mutate19IntermediateLevelApprenticeshipFM81(MessageLearner learner, bool valid)
        {
            Mutate19FM81(learner, valid);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First();
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate19HigherLevelApprenticeship4FM81(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel4).First();
//            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2014-AUG-01");
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            SetToOtherAdult(learner);
        }

        private void Mutate19HigherLevelApprenticeship5(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel5).First();
            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate19HigherLevelApprenticeship6(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel6).First();
            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate19HigherLevelApprenticeship6Restart(MessageLearner learner, bool valid)
        {
            Mutate19HigherLevelApprenticeship6(learner, valid);
            Helpers.AddRestartFAMToLearningDelivery(learner);
        }

        private void Mutate19FM81(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
            SetToOtherAdult(learner);
        }

        private static void SetToOtherAdult(MessageLearner learner)
        {
            learner.LearningDelivery[0].FundModel = (int)FundModel.OtherAdult;
            learner.LearningDelivery[1].FundModel = (int)FundModel.OtherAdult;
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderFullyFunded(learner);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddYears(1), Helpers.SetAchDate.SetAchDate);
            learner.LearningDelivery[0].AchDateSpecified = false;
            Helpers.SetEndDates(learner.LearningDelivery[1], learner.LearningDelivery[0].LearnStartDate.AddYears(1), Helpers.SetAchDate.SetAchDate);
            learner.LearningDelivery[1].AchDateSpecified = false;

            if (!valid)
            {
                Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(180), Helpers.SetAchDate.SetAchDate);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsStandards(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.OverrideLearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY-1|AUG|01]"));
            options.LD.IncludeHEFields = true;
            options.CreateDestinationAndProgression = true;
            options.LD.IncludeHHS = true;
            _options = options;
        }
    }
}
