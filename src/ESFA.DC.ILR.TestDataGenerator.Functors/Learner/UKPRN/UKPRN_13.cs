using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class UKPRN_13
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
            return "UKPRN_13";
        }

        public string LearnerReferenceNumberStub()
        {
            return "UKPRN13";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateOptionsInvalid },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLDM, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnActEndDate, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2018, 01, 01).AddDays(1);
            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractESFA).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateRestarts(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateLDM(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_Pilot).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2018, 01, 01).AddDays(-1);
            }
        }

        private void MutateLearnActEndDate(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.LearningDelivery[0].LearnActEndDateSpecified = true;
            learner.LearningDelivery[0].LearnActEndDate = new DateTime(2018, 08, 01).AddDays(-1);
        }

        private void MutateOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.AEBC).UKPRN;
        }

        private void MutateOptionsInvalid(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }
    }
}
