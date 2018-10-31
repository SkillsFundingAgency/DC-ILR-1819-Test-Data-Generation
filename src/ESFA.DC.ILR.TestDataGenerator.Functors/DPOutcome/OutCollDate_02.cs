using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OutCollDate_02
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
            return "OutCollDate_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutColDt02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutColDate, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutColDate, ExclusionRecord = true },
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

        // Check the teaching year start date for FM25 and include the below scenario if required
        private void MutateProgressionFM25(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dp = learner.DPOutcome[0];
            dp.OutCollDate = dp.OutStartDate.AddDays(60);
            if (!valid)
            {
                var startDate = new DateTime(2018, 08, 31);
                dp.OutCollDate = startDate.AddYears(-10).AddDays(-1);
            }
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dp = learner.DPOutcome[0];
            dp.OutCollDate = dp.OutStartDate.AddDays(60);
            if (!valid)
            {
                var startDate = new DateTime(2018, 08, 01);
                dp.OutCollDate = startDate.AddYears(-10).AddDays(-1);
            }
        }

        private void MutateProgressionOutColDate(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dp = learner.DPOutcome[0];
            dp.OutCollDate = dp.OutStartDate.AddDays(60);
            if (!valid)
            {
                var startDate = new DateTime(2018, 08, 01);
                dp.OutCollDate = startDate.AddYears(-10).AddDays(+1);
            }
        }
    }
}
