using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_07
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
            return "AFinType_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAimType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateCommon(MessageLearner learner)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.Apprenticeships);
            Helpers.SetApprenticeshipAims(learner, pta);

            Helpers.AddAfninRecord(
                learner,
                LearnDelAppFinType.PMR.ToString(),
                (int)LearnDelAppFinCode.TotalAssessmentPrice,
                500);
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            MutateCommon(learner);

            if (!valid)
            {
                learner.LearningDelivery[0].AppFinRecord = learner.LearningDelivery[0].AppFinRecord
                    .Where(aft => aft.AFinType != LearnDelAppFinType.TNP.ToString()).ToArray();
                Helpers.AddAfninRecord(
                    learner,
                    LearnDelAppFinType.TNP.ToString(),
                    (int)LearnDelAppFinCode.TotalTrainingPrice,
                    500);
            }
        }

        private void MutateAimType(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);

            if (!valid)
            {
                learner.LearningDelivery[0].AimType = (int)AimType.CoreAim1619;
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);

            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = (int)ProgType.IntermediateLevelApprenticeship;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
