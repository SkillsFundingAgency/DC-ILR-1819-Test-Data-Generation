using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_28
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DateOfBirth_28";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_28";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16IntermediateLevelApprenticeship, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship4, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship6, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16HigherLevelApprenticeship6Restart, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true }
            };
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

        private void Mutate16HigherLevelApprenticeship6Restart(MessageLearner learner, bool valid)
        {
            Mutate16HigherLevelApprenticeship6(learner, valid);
            Helpers.AddLearningDeliveryRestartFAM(learner);
        }

        private void Mutate16(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2013-AUG-01");
            MutateCommon(learner, valid);
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Fully);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate.AddDays(30);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
        }
    }
}
