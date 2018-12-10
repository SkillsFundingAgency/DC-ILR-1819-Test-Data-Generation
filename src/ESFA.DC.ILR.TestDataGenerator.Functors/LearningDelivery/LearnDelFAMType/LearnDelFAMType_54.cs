using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_54
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
            return "LearnDelFAMType_54";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy54";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate1, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateEEF(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FFI, LearnDelFAMCode.FFI_Fully);
            }
        }

        private void MutateFFI(MessageLearner learner, bool valid)
        {
            MutateEEF(learner, valid);
            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
            }
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetApprenticeshipAims(learner, pta);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 05, 01).AddDays(-1); // FundModel_07
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
            }
        }

        private void Mutate19Standard(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
        }

        private void MutateIntermediateLevelApprenticeship(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First();
            Helpers.SetApprenticeshipAims(learner, pta);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.FundModel = (int)FundModel.OtherAdult;
            }

            if (valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FFI, LearnDelFAMCode.FFI_Fully);
            }
        }

        private void MutateApprenticeshipStandards(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetApprenticeshipAims(learner, pta);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 05, 01).AddDays(-1); // FundModel_07
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)ProgType.IntermediateLevelApprenticeship;
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            MutateEEF(learner, valid);
            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FFI, LearnDelFAMCode.FFI_Co);
            }
        }

        private void Mutate1(MessageLearner learner, bool valid)
        {
            MutateEEF(learner, valid);
            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FFI, LearnDelFAMCode.FFI_Co);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
