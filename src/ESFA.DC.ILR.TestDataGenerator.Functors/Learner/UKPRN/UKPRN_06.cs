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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateConRefNumber, DoMutateOptions = MutatePLBGOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLDMOlass, DoMutateOptions = MutatePLBGOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeship, DoMutateOptions = MutatePLBGOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLDMAEB, DoMutateOptions = MutatePLBGOptions, ExclusionRecord = true }
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
                learner.LearningDelivery[0].ConRefNumber = "AEC-2307";
            }
        }

        private void MutateConRefNumber(MessageLearner learner, bool valid)
        {
            MutateLES(learner, valid);

            if (!valid)
            {
                learner.LearningDelivery[0].ConRefNumber = "ALLB-4051";
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
            MutateLES(learner, valid);
        }

        private void MutateLDMAEB(MessageLearner learner, bool valid)
        {
            MutateLDMOlass(learner, valid);
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_ProcuredAdultEducationBudget);
        }

        private void MutatePLBGOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.AEBC).UKPRN;
        }
    }
}
