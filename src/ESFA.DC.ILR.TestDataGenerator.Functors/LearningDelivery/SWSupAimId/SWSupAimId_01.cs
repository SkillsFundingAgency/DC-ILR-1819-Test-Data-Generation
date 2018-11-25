using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class SWSupAimId_01
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
            return "SWSupAimId_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "SSAimId01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (valid)
            {
                learner.LearningDelivery[0].SWSupAimId = Guid.NewGuid().ToString();
            }

            if (!valid)
            {
                var uuid = Guid.NewGuid().ToString().Split('-')[0];
                learner.LearningDelivery[0].SWSupAimId = uuid;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
