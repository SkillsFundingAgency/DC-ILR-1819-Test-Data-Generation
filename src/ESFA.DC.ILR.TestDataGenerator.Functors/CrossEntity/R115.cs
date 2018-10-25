using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R115
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
            return "R115";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R115";
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

        private void MutateCommonOne(MessageLearner learner, int learningdelivery, int Amt)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[learningdelivery];

            var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
            appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = Amt,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.PMR.ToString(),
                AFinCode = (int)LearnDelAppFinCode.AssessmentPayment,
                AFinCodeSpecified = true,
                AFinDate = ld.LearnStartDate,
                AFinDateSpecified = true
            });
            ld.AppFinRecord = appfin.ToArray();
        }

        private void MutateCommonTwo(MessageLearner learner, int learningdelivery, int Amt)
        {
            var ld = learner.LearningDelivery[learningdelivery];
            var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
            appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = Amt,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.PMR.ToString(),
                AFinCode = (int)LearnDelAppFinCode.ResidualTrainingPrice,
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
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = 1;
                    ld.AimTypeSpecified = true;
                    var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                    appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                    {
                        AFinAmount = 500,
                        AFinAmountSpecified = true,
                        AFinType = LearnDelAppFinType.PMR.ToString(),
                        AFinCode = (int)LearnDelAppFinCode.ResidualTrainingPrice,
                        AFinCodeSpecified = true,
                        AFinDate = ld.LearnStartDate,
                        AFinDateSpecified = true
                    });

                    ld.AppFinRecord = appfin.ToArray();
                }
            }
        }

        private void MutateAimType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateCommonOne(learner, 0, 1000);
                MutateCommonTwo(learner, 1, 500);

                //var lds = learner.LearningDelivery.ToList();
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                    ld.ProgTypeSpecified = true;
                    ld.AimType = 1;
                    ld.StdCode = 12;
                    ld.StdCodeSpecified = true;
                    ld.PwayCode = 1;
                    ld.PwayCodeSpecified = true;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
