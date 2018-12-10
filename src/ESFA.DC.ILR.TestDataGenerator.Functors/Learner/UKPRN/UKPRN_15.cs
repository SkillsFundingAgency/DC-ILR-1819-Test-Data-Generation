using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class UKPRN_15
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
            return "UKPRN_15";
        }

        public string LearnerReferenceNumberStub()
        {
            return "UKPRN15";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnActEndDate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLdmPilot, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLdmAct, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateOptionsValid, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractESFA).ToString()
                });
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_Pilot).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateLearnActEndDate(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].LearnActEndDateSpecified = true;
            learner.LearningDelivery[0].LearnActEndDate = new DateTime(2018, 08, 01).AddDays(-1);
        }

        private void MutateLdmPilot(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ld.LearningDeliveryFAM =
                    ldFams.Where(l => l.LearnDelFAMType != LearnDelFAMType.LDM.ToString()).ToArray();
            }
        }

        private void MutateLdmAct(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ld.LearningDeliveryFAM =
                    ldFams.Where(l => l.LearnDelFAMType != LearnDelFAMType.ACT.ToString()).ToArray();
            }
        }

        private void MutateOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.AEBC).UKPRN;
        }

        private void MutateOptionsValid(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }
    }
}
