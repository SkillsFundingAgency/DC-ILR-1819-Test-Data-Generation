using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ContPrefType_03 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
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
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "ContPr_03";
        }
    }
}
