using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AchDate_07
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
            return "AchDate_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AchDt07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var lds = learner.LearningDelivery.ToList();
                Helpers.SetLearningDeliveryEndDates(lds[0], lds[0].LearnStartDate.AddDays(30), Helpers.SetAchDate.SetAchDate);
                lds[0].AchDate = DateTime.Today.AddDays(1);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
