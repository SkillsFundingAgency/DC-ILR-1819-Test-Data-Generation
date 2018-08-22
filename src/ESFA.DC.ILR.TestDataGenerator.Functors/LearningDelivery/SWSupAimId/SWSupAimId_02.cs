using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class SWSupAimId_02
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
            return "SWSupAimId_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "SSAimId02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                learner.LearningDelivery[0].SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            var uuid = string.Empty;

            if (!valid)
            {
                var guid = Guid.NewGuid().ToString();
                foreach (char c in guid)
                {
                    uuid = uuid + " ";
                }

                learner.LearningDelivery[0].SWSupAimId = uuid;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
