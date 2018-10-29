using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class UKPRN_09
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
            return "UKPRN_09";
        }

        public string LearnerReferenceNumberStub()
        {
            return "UKPRN09";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateOptionsInvalid, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateOlass, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateNonApprenticeshipSportingExcellence, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateNonApprenticeshipTheatre, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnActEndDate, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateNonFundedApprenticeshipStandard, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetApprenticeshipAims(learner, pta);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 05, 01).AddDays(-1); // FundModel_07
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
        }

        private void MutateNonFundedApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.NonFunded);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetApprenticeshipAims(learner, pta);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 05, 01).AddDays(-1); // FundModel_07
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
        }

        private void MutateOlass(MessageLearner learner, bool valid)
        {
            MutateApprenticeshipStandard(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_OLASS).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateNonApprenticeshipSportingExcellence(MessageLearner learner, bool valid)
        {
            MutateApprenticeshipStandard(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_NonApprenticeshipSportingExcellence).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateNonApprenticeshipTheatre(MessageLearner learner, bool valid)
        {
            MutateApprenticeshipStandard(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                var ldFams = ld.LearningDeliveryFAM.ToList();
                ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_NonApprenticeshipTheatre).ToString()
                });
                ld.LearningDeliveryFAM = ldFams.ToArray();
            }
        }

        private void MutateLearnActEndDate(MessageLearner learner, bool valid)
        {
            MutateApprenticeshipStandard(learner, valid);
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
