using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class StdCode_03
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
            return "StdCode_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "StdCode03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].StdCodeSpecified = true;
                learner.LearningDelivery[0].StdCode = 1;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
