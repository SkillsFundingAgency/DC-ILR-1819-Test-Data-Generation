using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ContPrefType_03 : ILearnerMultiMutator
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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateRUI3, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public void MutateRUI3(MessageLearner learner, bool valid)
        {
            learner.ContactPreference = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactOldDied,
                    ContPrefCodeSpecified = true
                }
            }.ToArray();
            if (valid)
            {
                learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2013-JUL-30");
            }

            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(
                (FundModel)learner.LearningDelivery[0].FundModel,
                LearnDelFAMCode.SOF_ESFA_1619,
                learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "ContPrefType_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ContPr_03";
        }
    }
}
