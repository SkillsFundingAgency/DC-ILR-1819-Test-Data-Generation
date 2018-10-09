using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EPAOrgID_03
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
            return "EPAOrgID_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EOrgId03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateTotalTrainingPrice, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateResidualTrainingPrice, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateTotalAssessPrice, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateCommon(MessageLearner learner, bool valid, LearnDelAppFinCode learnDelAppFinCode)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[0];
            if (!valid)
            {
                learner.LearningDelivery[0].EPAOrgID = "EPA1234";
                Helpers.AddAfninRecord(learner, LearnDelAppFinType.TNP.ToString(), (int)learnDelAppFinCode, 500);
            }
        }

        private void MutateTotalTrainingPrice(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, LearnDelAppFinCode.TotalTrainingPrice);
        }

        private void MutateResidualTrainingPrice(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, LearnDelAppFinCode.ResidualTrainingPrice);
        }

        private void MutateTotalAssessPrice(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, LearnDelAppFinCode.TotalAssessmentPrice);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
