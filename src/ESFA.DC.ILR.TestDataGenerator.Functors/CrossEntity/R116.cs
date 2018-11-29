using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R116
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "R116";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R116";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateApprenticeship(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                Helpers.AddAfninRecord(learner, "PMR", 1, 1000);
                Helpers.AddAfninRecord(learner, "PMR", 3, 500);
            }

            if (!valid)
            {
                Helpers.AddAfninRecord(learner, "PMR", 1, 1000);
                Helpers.AddAfninRecord(learner, "PMR", 3, 500);
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                Helpers.AddAfninRecord(learner, "PMR", 1, 1000);
                Helpers.AddAfninRecord(learner, "PMR", 3, 500);
            }

            if (!valid)
            {
                Helpers.AddAfninRecord(learner, "PMR", 1, 1000);
                Helpers.AddAfninRecord(learner, "PMR", 3, 1500);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
