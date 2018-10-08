using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R102
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "R102";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R102";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLD2Restart, DoMutateOptions = MutateGenerationOptionsLD2 }
            };
        }

        private void MutateLD2Restart(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnStartDate = new DateTime(2017, 08, 06);
            }
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            options.EmploymentRequired = true;
            _options = options;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
