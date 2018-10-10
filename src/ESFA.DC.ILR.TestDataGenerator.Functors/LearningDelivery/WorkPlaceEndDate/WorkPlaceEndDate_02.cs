using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class WorkPlaceEndDate_02
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
            return "WorkPlaceEndDate_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "WrkPlEn02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsSOF }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "Z0007834";
            if (valid)
            {
                MutateWorkPlacement(learner, -1);
            }

            if (!valid)
            {
                MutateWorkPlacement(learner, 1);
            }
        }

        private void MutateWorkPlacement(MessageLearner learner, int days)
        {
            learner.LearningDelivery[0].LearnActEndDateSpecified = true;
            learner.LearningDelivery[0].LearnActEndDate = learner.LearningDelivery[0].LearnStartDate.AddDays(60);
            learner.LearningDelivery[0].CompStatus = (int)CompStatus.Completed;
            learner.LearningDelivery[0].OutcomeSpecified = true;
            learner.LearningDelivery[0].Outcome = (int)Outcome.Achieved;
            var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = learner.LearningDelivery[0].LearnStartDate.AddDays(30),
                        WorkPlaceEndDateSpecified = true,
                        WorkPlaceEndDate = learner.LearningDelivery[0].LearnActEndDate.AddDays(days),
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 1000,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 900271388
                    }
                };
                learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
