using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Outcome_09
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
            return "Outcome_09";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutCom09";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCompStatusCompleted, DoMutateOptions = MutateGenerationOptionsDestProg }
            };
        }

        private void MutateCompStatusCompleted(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.NotYetKnown;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddMonths(3);

            if (valid)
            {
                ld[0].CompStatus = (int)CompStatus.Completed;
            }

            if (!valid)
            {
                ld[0].CompStatus = (int)CompStatus.Withdrawn;
            }
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
