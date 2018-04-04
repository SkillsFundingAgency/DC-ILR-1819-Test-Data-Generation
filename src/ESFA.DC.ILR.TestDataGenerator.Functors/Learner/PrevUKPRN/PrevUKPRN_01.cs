using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PrevUKPRN_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;
        private long _prevUKPRN;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PUKPRN_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.PrevUKPRN = _prevUKPRN;
            learner.PrevUKPRNSpecified = true;
            if (!valid)
            {
                learner.PrevUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.NotExist).UKPRN;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _prevUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.PartnerOrganisation).UKPRN;
        }
    }
}
