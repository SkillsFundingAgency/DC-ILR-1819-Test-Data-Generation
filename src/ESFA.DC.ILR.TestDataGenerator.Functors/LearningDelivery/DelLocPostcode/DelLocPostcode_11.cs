using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DelLocPostcode_11
        : ILearnerMultiMutator
    {
        private readonly List<string> _nonExistPostcodes = new List<string> { "CV2 11UC", "CV21 1VA", "GY12 1SO" };
        private readonly List<string> _validPostcodes = new List<string> { "CV21 1RL", "CV1 1DX", "CV1 2AY" };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DelLocPostcode_11";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DLPCode11";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutatePostOne, DoMutateOptions = MutateGenerationOptions }
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
                learner.LearningDelivery[0].DelLocPostCode = _nonExistPostcodes[1];
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
