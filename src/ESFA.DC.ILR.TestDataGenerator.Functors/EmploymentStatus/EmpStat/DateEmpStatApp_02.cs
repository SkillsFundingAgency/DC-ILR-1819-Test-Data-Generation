using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateEmpStatApp_02
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
            return "DateEmpStatApp_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DtEmpStA02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEmpStatus, DoMutateOptions = MutateGenerationOptionsEmpStatus },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateEmpStatus, DoMutateOptions = MutateGenerationOptionsEmpStatus },
            };
        }

        private void MutateEmpStatus(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var empstat = learner.LearnerEmploymentStatus.ToList();
            foreach (var les in empstat)
            {
                les.DateEmpStatAppSpecified = true;
                if (valid)
                {
                    les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
                }

                if (!valid)
                {
                    les.DateEmpStatApp = new DateTime(1990, 08, 01).AddDays(-1);
                }
            }
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            //ProgType_13
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2014, 07, 30);
                Helpers.MutateApprenticeToTrainee(learner, _dataCache);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HHS, LearnDelFAMCode.HHS_SingleWithChildren);
                MutateEmpStatus(learner, valid);
            }
        }

        private void MutateTApprenticeship(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            // FundModel_05 and FundModel_08
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2014, 07, 30);
                MutateEmpStatus(learner, valid);
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.ProgrammeAim;
                }

                MutateEmpStatus(learner, valid);
            }
        }

        private void MutateEmpstatusNull(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var empstat = learner.LearnerEmploymentStatus.ToList();
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2014, 07, 30);
                _options.EmploymentRequired = false;
                foreach (var les in learner.LearnerEmploymentStatus)
                {
                    learner.LearnerEmploymentStatus =
                        empstat.Where(dt => dt.DateEmpStatApp == new DateTime(2015, 06, 10)).ToArray();
                }
            }
        }

        private void MutateStartDate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                led.LearnStartDate = new DateTime(2014, 08, 01);
            }

            MutateEmpStatus(learner, valid);
        }

        private void MutateGenerationOptionsEmpStatus(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
