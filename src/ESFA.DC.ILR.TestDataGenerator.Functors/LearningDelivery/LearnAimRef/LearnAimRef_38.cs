using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_38
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
            return "LearnAimRef_38";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimRef38";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateNoADL, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateNoADL, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateADL, DoMutateOptions = MutateGenerationOptionsADL, ExclusionRecord = true }
            };
        }

        private void MutateNoADL(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60148743";
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2015, 01, 01).AddDays(-1);
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60148743";
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2015, 01, 01).AddDays(-1);
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = 25;
            }
        }

        private void MutatRotl(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60148743";
            }
        }

        private void MutateADL(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[0];

            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;

            if (!valid)
            {
                ld.LearnAimRef = "60148743";
                ld.LearnStartDate = new DateTime(2015, 01, 01).AddDays(-1);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsADL(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeADL = true;
        }
    }
}
