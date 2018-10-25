using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R90
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "R90";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R90";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLD2Restart, DoMutateOptions = MutateGenerationOptionsLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void MutateLD2Restart(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnStartDate = new DateTime(2017, 08, 06);
                //lds[0].LearnActEndDate = new DateTime(2018, 08, 09);
                lds[0].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[0].LearnActEndDateSpecified = true;

                lds[1].LearnStartDate = new DateTime(2017, 11, 06);
                //lds[1].LearnActEndDate = new DateTime(2018, 08, 09);
                lds[1].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[1].LearnActEndDateSpecified = false;

                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TrainingPayment,
                    AFinCodeSpecified = true,
                    AFinDate = learner.LearningDelivery[0].LearnStartDate,
                    AFinDateSpecified = true
                });

                learner.LearningDelivery[0].AppFinRecord = appfin.ToArray();
                lds[0].LearnActEndDate = new DateTime(2018, 08, 08);
            }
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            options.EmploymentRequired = true;
            _options = options;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnAimRef = "ZPROG001";
                lds[0].AimType = 1;
                lds[0].LearnStartDate = new DateTime(2017, 08, 06);
                lds[0].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[0].LearnActEndDateSpecified = true;

                lds[1].LearnAimRef = "10019777"; //"60170530";
                lds[1].AimType = 3;
                lds[1].LearnStartDate = new DateTime(2017, 11, 06);
                lds[1].LearnActEndDate = new DateTime(2018, 08, 09);
                lds[1].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[1].LearnActEndDateSpecified = true;

                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TotalAssessmentPrice,
                    AFinCodeSpecified = true,
                    AFinDate = learner.LearningDelivery[0].LearnStartDate,
                    AFinDateSpecified = true
                });

                learner.LearningDelivery[0].AppFinRecord = appfin.ToArray();
                lds[0].LearnActEndDate = new DateTime(2018, 08, 08);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateOptions(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            options.EmploymentRequired = true;
        }
    }
}
