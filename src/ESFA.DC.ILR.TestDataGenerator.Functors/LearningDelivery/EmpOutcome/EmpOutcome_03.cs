using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpOutcome_03
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private List<LearnDelFAMCode> _overrideLDMCodes = new List<LearnDelFAMCode>() { LearnDelFAMCode.LDM_NonApprenticeshipSportingExcellence, LearnDelFAMCode.LDM_NonApprenticeshipTheatre, LearnDelFAMCode.LDM_NonApprenticeshipSeaFishing };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _overrideLDMCodes = new List<LearnDelFAMCode>() { LearnDelFAMCode.LDM_NonApprenticeshipSportingExcellence, LearnDelFAMCode.LDM_NonApprenticeshipTheatre, LearnDelFAMCode.LDM_NonApprenticeshipSeaFishing };
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19IntermediateLevelApprenticeship, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship4, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship6, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Standard, DoMutateOptions = MutateGenerationOptionsStandards, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Trainee, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Restart, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptionsLDM, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptionsLDM, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptionsLDM, InvalidLines = 2 },
            };
        }

        public string RuleName()
        {
            return "EmpOutcome_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpOut03";
        }

        private void Mutate19Trainee(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeToTrainee(learner, _dataCache);
            MutateValid(learner, valid);
        }

        private void Mutate19Standard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void MutateValid(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.EmpOutcomeSpecified = true;
                    ld.EmpOutcome = 1;
                }
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
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19HigherLevelApprenticeship5(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel5).First();
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19HigherLevelApprenticeship6(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel6).First();
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
            MutateValid(learner, valid);
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
            MutateValid(learner, valid);
        }

        private void Mutate19Restart(MessageLearner learner, bool valid)
        {
            Helpers.AddLearningDeliveryRestartFAM(learner);
            MutateCommon(learner, valid);
            MutateValid(learner, valid);
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Fully);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHHS = true;
            _options = options;
        }

        private void MutateGenerationOptionsLDM(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)_overrideLDMCodes[0];
            _overrideLDMCodes.RemoveAt(0);
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
