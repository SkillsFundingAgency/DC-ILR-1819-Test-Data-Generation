using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    /// <summary>
    /// The FM25 test data generator helps create a range of high quality test data specifically to test the FM25 funding model
    /// It starts with simple learners but becomes more and more complex in what the earning calculation has to do and areas where in the past there have been specific
    /// issues and bugs
    /// </summary>
    public class FM25_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateRUI12, DoMutateOptions = MutateGenerationOptions },
            };
        }

        public string RuleName()
        {
            return "FM25_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "fm25_01";
        }

        public void MutateRUI12(MessageLearner learner, bool valid)
        {
            learner.ContactPreference = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactCourses,
                    ContPrefCodeSpecified = true
                },
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactSurvey,
                    ContPrefCodeSpecified = true
                }
            }.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
