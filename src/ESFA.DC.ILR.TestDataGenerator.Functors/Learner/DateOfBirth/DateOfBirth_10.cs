using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_10
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16IntermediateLevelApprenticeship, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship4, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship6, DoMutateOptions = MutateGenerationOptionsHE, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16Trainee, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true }
            };
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
        }

        public string RuleName()
        {
            return "DateOfBirth_10";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_10";
        }

        private void Mutate16Trainee(MessageLearner learner, bool valid)
        {
            Mutate16(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.FworkCodeSpecified = false;
                ld.PwayCodeSpecified = false;
                Helpers.SetLearningDeliveryEndDates(ld, DateTime.Parse("2013-NOV-30"), Helpers.SetAchDate.SetAchDate);
            }
        }

        private void Mutate16IntermediateLevelApprenticeship(MessageLearner learner, bool valid)
        {
            Mutate16(learner, valid);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First();
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate16HigherLevelApprenticeship4(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel4).First();
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2013-AUG-01");
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate16HigherLevelApprenticeship5(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel5).First();
            learner.LearningDelivery[0].LearnStartDate = (DateTime)pta.Validity[0].From;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate16HigherLevelApprenticeship6(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel6).First();
            learner.LearningDelivery[0].LearnStartDate = (DateTime)pta.Validity[0].From;
            MutateCommon(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate16(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2013-AUG-01");
            MutateCommon(learner, valid);
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Fully);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less16And30Days, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.YoungerLots);
        }
    }
}
