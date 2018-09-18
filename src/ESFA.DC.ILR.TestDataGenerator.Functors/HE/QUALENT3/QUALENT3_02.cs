using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class QUALENT3_02
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "QUALENT3_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Qent3_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner1, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        public void MutateLearner(MessageLearner learner, bool valid)
        {
           Helpers.AddLearningDeliveryHE(learner);

            if (valid)
            {
                Helpers.AddLearningDeliveryHE(learner);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryHE(learner, "XYZ");
            }
        }

        public void MutateLearner1(MessageLearner learner, bool valid)
        {
            Helpers.AddLearningDeliveryHE(learner);

            if (valid)
            {
                Helpers.AddLearningDeliveryHE(learner);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryHE(learner, "X03");
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
