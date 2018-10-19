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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearner81, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner36, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateLearner36(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery;
            foreach (MessageLearnerLearningDelivery del in lds)
            {
                del.SWSupAimId = Guid.NewGuid().ToString();
            }

            if (!valid)
            {
                learner.LearningDelivery[0].AimType = (long)AimType.ComponentAim;
                MutateCommon(learner, valid);
            }
        }

        private void MutateLearner81(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            if (!valid)
            {
                ld.ProgType = (long)ProgType.AdvancedLevelApprenticeship;
                MutateCommon(learner, valid);
            }
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
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

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
