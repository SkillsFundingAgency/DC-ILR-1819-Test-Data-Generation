using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class UKPRN_06
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
            return "UKPRN_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "UKPRN06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLES, DoMutateOptions = MutateOptionsInvalid },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMOlass, DoMutateOptions = MutateOptionsInvalid },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMAEB, DoMutateOptions = MutateOptionsInvalid },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateActEndDate, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeship, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateDD07, DoMutateOptions = MutateOptionsInvalid, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLES, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMOlass, DoMutateOptions = MutateOptions, ExclusionRecord = true },
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            if (valid)
            {
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = (int)EmploymentStatus.PaidEmployment;
            }
        }

        private void MutateLDMOlass(MessageLearner learner, bool valid)
        {
            MutateLES(learner, valid);

            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_OLASS);
            }
        }

        private void MutateApprenticeship(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateLES(learner, valid);
            }
        }

        private void MutateLDMAEB(MessageLearner learner, bool valid)
        {
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_ProcuredAdultEducationBudget);
        }

        private void MutateDD07(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = (int)ProgType.IntermediateLevelApprenticeship;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_ProcuredAdultEducationBudget);
            }
        }

        private void MutateActEndDate(MessageLearner learner, bool valid)
        {
            MutateLES(learner, valid);
            if (!valid)
            {
                _options.CreateDestinationAndProgression = true;
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 06, 01);
                learner.LearningDelivery[0].LearnActEndDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(3);
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(3);
                learner.LearningDelivery[0].LearnActEndDateSpecified = true;
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_ProcuredAdultEducationBudget);
            }
        }

        private void MutateOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutateOptionsInvalid(GenerationOptions options)
        {
            _options = options;
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.PLBG).UKPRN;
        }
    }
}
