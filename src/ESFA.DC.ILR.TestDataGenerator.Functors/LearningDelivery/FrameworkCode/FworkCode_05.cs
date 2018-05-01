using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FworkCode_05
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19AdvancedLevelApprenticeship, DoMutateOptions = MutateGenerationOptions, InvalidLines = 1 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19IntermediateLevelApprenticeship, DoMutateOptions = MutateGenerationOptions, InvalidLines = 1 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship4, DoMutateOptions = MutateGenerationOptions, InvalidLines = 1 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 1 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship6, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 1 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Standard, DoMutateOptions = MutateGenerationOptionsStandards, InvalidLines = 2, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Trainee, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 2, ExclusionRecord = true },
            };
        }

        public string RuleName()
        {
            return "FworkCode_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "fwc_05";
        }

        private void Mutate19Trainee(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.MutateApprenticeToTrainee(learner, _dataCache);
            //Mutate19(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = DateTime.Parse("2018-SEP-01");
                ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(45);
                if (!valid)
                {
                    ld.FworkCodeSpecified = true;
                    ld.FworkCode = 12;
                }
            }
        }

        private void Mutate19Standard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetLearningDeliveryEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(372), Helpers.SetAchDate.SetAchDate);
            Helpers.SetLearningDeliveryEndDates(learner.LearningDelivery[1], learner.LearningDelivery[0].LearnActEndDate, Helpers.SetAchDate.DoNotSetAchDate);
            Helpers.SetApprenticeshipAims(learner, pta);

            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    ld.FworkCodeSpecified = true;
                    ld.FworkCode = 12;
                }
            }
        }

        private void Mutate19AdvancedLevelApprenticeship(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First();
            Helpers.SetApprenticeshipAims(learner, pta);
            if (!valid)
            {
                learner.LearningDelivery[1].LearnAimRef = _dataCache.LearningDeliveryWithCommonComponent(CommonComponent.SteppingStoneEnglish).LearnAimRef;
            }
        }

        private void Mutate19IntermediateLevelApprenticeship(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First();
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19HigherLevelApprenticeship4(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel4).First();
            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19HigherLevelApprenticeship5(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel5).First();
            learner.LearningDelivery[0].LearnStartDate = (DateTime)pta.Validity[0].From;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19HigherLevelApprenticeship6(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel6).First();
            learner.LearningDelivery[0].LearnStartDate = (DateTime)pta.Validity[0].From;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
            MutateValid(learner, valid);
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderFullyFunded(learner);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
        }

        private void MutateValid(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    if (ld.AimType == (int)AimType.ComponentAim)
                    {
                        ld.FworkCode++;
                    }
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.OverrideLearnStartDate = DateTime.Parse("2016-AUG-01");
            options.LD.IncludeHHS = true;
            _options = options;
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            MutateGenerationOptions(options);
        }

        private void MutateGenerationOptionsStandards(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            MutateGenerationOptions(options);
        }
    }
}
