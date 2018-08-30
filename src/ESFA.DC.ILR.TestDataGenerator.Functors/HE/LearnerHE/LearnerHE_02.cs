using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnerHE_02 :
        ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LearnerHE_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LearnHE02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLDHE, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnerHE, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true }
            };
        }

        public void Mutate(MessageLearner learner, bool valid)
        {
            var lhe = new List<MessageLearnerLearnerHE>();
            lhe.Add(new MessageLearnerLearnerHE()
            {
                UCASPERID = "9999911111",
            });
            learner.LearnerHE = lhe.ToArray();
        }

        public void MutateLDHE(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearningDeliveryHE = null;
            }
        }

        public void MutateLearnerHE(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            if (!valid)
            {
                learner.LearnerHE = null;
            }
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.EmploymentRequired = true;
        }
    }
}
