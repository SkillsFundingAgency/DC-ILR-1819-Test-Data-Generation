using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R58
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
            return "R58";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R58";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLD2Restart, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void MutateLD2Restart(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                var ld = learner.LearningDelivery.ToList();
                learner.LearningDelivery = ld.Skip(1).ToArray();
                learner.LearningDelivery[0].AimSeqNumberSpecified = true;
                learner.LearningDelivery[0].AimSeqNumber = 1;
            }

            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                    lds[0].AimType = 5;
                lds[0].LearnStartDate = new DateTime(2017, 08, 06);
                lds[0].LearnActEndDate = new DateTime(2018, 08, 08);
                lds[0].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[0].LearnActEndDateSpecified = true;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.Achieved;
                lds[0].CompStatus = (int)CompStatus.Completed;
                lds[1].LearnAimRef = "10019777";
                lds[1].AimType = 5;
                lds[1].LearnStartDate = new DateTime(2017, 11, 06);
                lds[1].LearnActEndDate = new DateTime(2018, 08, 08);
                lds[1].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[1].LearnActEndDateSpecified = true;
                lds[1].OutcomeSpecified = true;
                lds[1].Outcome = (int)Outcome.Achieved;
                lds[1].CompStatus = (int)CompStatus.Completed;
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
            if (valid)
            {
                var ld = learner.LearningDelivery.ToList();
                learner.LearningDelivery = ld.Skip(1).ToArray();
                learner.LearningDelivery[0].AimSeqNumberSpecified = true;
                learner.LearningDelivery[0].AimSeqNumber = 1;
            }

            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnAimRef = "ZPROG001";
                lds[0].AimType = 1;
                lds[0].LearnStartDate = new DateTime(2017, 08, 06);
                lds[0].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[0].LearnActEndDateSpecified = true;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.Achieved;
                lds[0].CompStatus = (int)CompStatus.Completed;

                lds[1].LearnAimRef = "10019777"; //"60170530";
                lds[1].AimType = 3;
                lds[1].LearnStartDate = new DateTime(2017, 11, 06);
                lds[1].LearnActEndDate = new DateTime(2018, 08, 08);
                lds[1].LearnPlanEndDate = new DateTime(2018, 08, 08);
                lds[1].LearnActEndDateSpecified = true;
                lds[1].OutcomeSpecified = true;
                lds[1].Outcome = (int)Outcome.Achieved;
                lds[1].CompStatus = (int)CompStatus.Completed;

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
            options.CreateDestinationAndProgression = true;
        }
    }
}
