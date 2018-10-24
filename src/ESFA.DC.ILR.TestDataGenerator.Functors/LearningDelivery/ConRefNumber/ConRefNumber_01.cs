using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ConRefNumber_01
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
            return "ConRefNumber_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ConRef01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsDestProg }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;

            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
                lds.ConRefNumber = "ESF-2010";
                if (!valid) { lds.ConRefNumber = "ES-111112010"; }
            }
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
        }
    }
}
