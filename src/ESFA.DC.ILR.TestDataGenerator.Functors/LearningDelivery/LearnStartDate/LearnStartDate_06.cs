using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnStartDate_06
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
            return "LearnStartDate_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LstartDt06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateRestart, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
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
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.PwayCode = 0;
                }
            }
        }

        private void MutateRestart(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateCommon(learner);
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.PwayCode = 0;
                }

                Helpers.AddLearningDeliveryRestartFAM(learner);
            }
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}