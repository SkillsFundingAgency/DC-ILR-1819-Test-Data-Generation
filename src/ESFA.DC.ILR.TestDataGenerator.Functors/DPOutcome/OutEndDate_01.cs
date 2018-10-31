using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OutEndDate_01
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
            return "OutEndDate_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutEndDt01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOOutEndDate, ExclusionRecord = true },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.Achieved;
            ld[0].CompStatus = (int)CompStatus.Completed;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddDays(60);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.CreateDestinationAndProgression = true;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dp = learner.DPOutcome[0];
            if (!valid)
            {
                dp.OutEndDateSpecified = true;
                dp.OutEndDate = dp.OutStartDate.AddDays(-1);
            }
        }

        private void MutateProgressionOOutEndDate(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dp = learner.DPOutcome[0];
            if (!valid)
            {
                dp.OutEndDateSpecified = false;
            }
        }
    }
}
