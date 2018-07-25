using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DelLocPostcode_16
        : ILearnerMultiMutator
    {
        private readonly List<string> _nonExistPostcodes = new List<string> { "CV21 1UU", "CV21 1VA", "GY12 1SW" };
        private readonly List<string> _validPostcodes = new List<string> { "CV21 1RL", "CV1 1DX", "CV1 2AY" };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DelLocPostcode_16";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DLPCode16";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutatePostOne, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.Postcode = _validPostcodes[0];
            if (!valid)
            {
                learner.LearningDelivery[0].DelLocPostCode = _nonExistPostcodes[0];
            }
        }

        private void MutatePostOne(MessageLearner learner, bool valid)
        {
            learner.Postcode = _validPostcodes[1];
            if (!valid)
            {
                learner.LearningDelivery[0].DelLocPostCode = _validPostcodes[2];
            }
        }

        private void MutatePostTwo(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].DelLocPostCode = _nonExistPostcodes[2];
                Helpers.MutateLearningDeliveryMonitoringLDMToNewCode(learner, LearnDelFAMCode.LDM_OLASS);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
