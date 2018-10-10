using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_11
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
            return "AFinType_11";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty11";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppCodeAssesment, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppCodeResidulAssesment, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateCommon(MessageLearner learner, LearnDelAppFinCode appfincode)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
            appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = 500,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.TNP.ToString(),
                AFinCode = (int)appfincode,
                AFinCodeSpecified = true,
                AFinDate = learner.LearningDelivery[0].LearnStartDate,
                AFinDateSpecified = true
            });

                learner.LearningDelivery[0].AppFinRecord = appfin.ToArray();
        }

        private void MutateAppCodeAssesment(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateCommon(learner, LearnDelAppFinCode.TotalTrainingPrice);
            }

            if (!valid)
            {
                MutateCommon(learner, LearnDelAppFinCode.AssessmentPayment);
            }
        }

        private void MutateAppCodeResidulAssesment(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateCommon(learner, LearnDelAppFinCode.TotalTrainingPrice);
            }

            if (!valid)
            {
                MutateCommon(learner, LearnDelAppFinCode.ResidualAssessmentPrice);
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AppFinRecord = null;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
