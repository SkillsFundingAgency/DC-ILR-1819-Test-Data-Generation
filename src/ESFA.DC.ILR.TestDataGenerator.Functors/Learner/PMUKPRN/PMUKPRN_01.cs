using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PMUKPRN_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;
        private long _pmUKPRN;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PMUKPRN_01";
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
            learner.PMUKPRN = _pmUKPRN;
            learner.PMUKPRNSpecified = true;
            if (!valid)
            {
                learner.PMUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.NotExist).UKPRN;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _pmUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.PartnerOrganisation).UKPRN;
        }
    }
}
