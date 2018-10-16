using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_14
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
            return "AFinType_14";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty14";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAimType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateCommon(MessageLearner learner, int learningdelivery)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[learningdelivery];

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

            appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = 500,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.PMR.ToString(),
                AFinCode = (int)LearnDelAppFinCode.TrainingPayment,
                AFinCodeSpecified = true,
                AFinDate = ld.LearnStartDate,
                AFinDateSpecified = true
            });

            ld.AppFinRecord = appfin.ToArray();
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, 0);

            if (!valid)
            {
                learner.LearningDelivery[0].AppFinRecord = learner.LearningDelivery[0].AppFinRecord
                    .Where(aft => aft.AFinType != LearnDelAppFinType.TNP.ToString()).ToArray();
            }
        }

        private void MutateAimType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateCommon(learner, 1);
                learner.LearningDelivery[1].AppFinRecord = learner.LearningDelivery[1].AppFinRecord
                    .Where(aft => aft.AFinType != LearnDelAppFinType.TNP.ToString()).ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
