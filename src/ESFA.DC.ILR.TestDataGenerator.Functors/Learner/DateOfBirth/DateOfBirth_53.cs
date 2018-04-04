using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_53
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
            return "DOB_53";
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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 13 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsRestart, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Standard, DoMutateOptions = MutateGenerationOptionsStandards, InvalidLines = 11, ExclusionRecord = true },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate.AddDays(364);
                Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(364), Helpers.SetAchDate.SetAchDate);
                Helpers.SetEndDates(learner.LearningDelivery[1], learner.LearningDelivery[0].LearnStartDate.AddDays(364), Helpers.SetAchDate.SetAchDate);
            }
        }

        private void Mutate19Standard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
//            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            Helpers.MutateApprenticeshipToStandard(learner);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(372), Helpers.SetAchDate.SetAchDate);
            Helpers.SetEndDates(learner.LearningDelivery[1], learner.LearningDelivery[0].LearnActEndDate, Helpers.SetAchDate.DoNotSetAchDate);

            if (!valid)
            {
                Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(364), Helpers.SetAchDate.SetAchDate);
            }

            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsRestart(GenerationOptions options)
        {
            options.LD.IncludeRES = true;
        }

        private void MutateGenerationOptionsStandards(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            options.LD.IncludeHHS = true;
            _options = options;
        }
    }
}
