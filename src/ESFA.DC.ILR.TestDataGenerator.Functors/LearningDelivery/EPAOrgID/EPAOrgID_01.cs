using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EPAOrgID_01
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
            return "EPAOrgID_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EOrgId01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEPA, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateEPA(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].EPAOrgID = "EPA0022";
            MutateCommon(learner, valid, LearnDelAppFinCode.TotalAssessmentPrice);
        }

        private void MutateCommon(MessageLearner learner, bool valid, LearnDelAppFinCode learnDelAppFinCode)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[0];
            Helpers.AddAfninRecord(learner, LearnDelAppFinType.TNP.ToString(), (int)learnDelAppFinCode, 500);
            foreach (var lde in learner.LearningDelivery)
            {
                lde.FworkCodeSpecified = false;
                lde.PwayCodeSpecified = false;
                lde.StdCodeSpecified = true;
                lde.StdCode = 26;
                lde.ProgTypeSpecified = true;
                lde.ProgType = (int)ProgType.ApprenticeshipStandard;
            }

            if (!valid)
            {
                learner.LearningDelivery[0].EPAOrgID = "EPA9999";
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
