using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class QUALENT3_03
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "QUALENT3_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Qent3_03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
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
                Helpers.AddLearningDeliveryHE(learner, "X03");
            }
        }

        public void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);

            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2013, 07, 31).AddDays(-1);
            }
        }

        public void MutateRestarts(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);

            if (!valid)
            {
                Helpers.AddLearningDeliveryRestartFAM(learner);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
