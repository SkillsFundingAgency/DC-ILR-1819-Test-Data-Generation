using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PartnerUKPRN_02
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PartnerUKPRN_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PartUKPRN02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsFullyFundedApprenticeships }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                    learner.LearningDelivery[0].PartnerUKPRNSpecified = true;
                    learner.LearningDelivery[0].PartnerUKPRN = 90000064;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutateGenerationOptionsFullyFundedApprenticeships(GenerationOptions options)
        {
            options.SetACTToFullyFunded = true;
        }
    }
}
