using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_04
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "AFinType_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateGenerationOptionsEmpStatus, ExclusionRecord = true },
            };
        }

        private void MutateCommon(MessageLearner learner)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[0];

                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TotalTrainingPrice,
                    AFinCodeSpecified = true,
                    AFinDate = ld.LearnStartDate,
                    AFinDateSpecified = true
                });

                ld.AppFinRecord = appfin.ToArray();
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateCommon(learner);
            }
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetApprenticeshipAims(learner, pta);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 05, 01).AddDays(-1); // FundModel_07
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
            MutateLearner(learner, valid);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsEmpStatus(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
