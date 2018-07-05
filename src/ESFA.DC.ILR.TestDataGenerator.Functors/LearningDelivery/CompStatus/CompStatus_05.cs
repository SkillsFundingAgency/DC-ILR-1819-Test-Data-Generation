using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class CompStatus_05
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
            return "CompStatus_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Compsts05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].CompStatusSpecified = true;
            lds[0].CompStatus = (int)CompStatus.Continuing;
            if (!valid)
            {
                Helpers.SetLearningDeliveryEndDates(lds[0], lds[0].LearnStartDate.AddDays(30), Helpers.SetAchDate.DoNotSetAchDate);
                lds[0].CompStatus = (int)CompStatus.Continuing;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
