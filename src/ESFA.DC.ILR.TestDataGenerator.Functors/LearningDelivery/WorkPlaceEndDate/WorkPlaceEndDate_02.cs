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
            return "WrkPlaceE02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsSOF },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearnerAimRef, DoMutateOptions = MutateGenerationOptionsSOF, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            DateTime edate;
            var ld = learner.LearningDelivery;

            ld[0].LearnPlanEndDate = new DateTime(2018, 9, 20);
            ld[0].LearnActEndDate = ld[0].LearnPlanEndDate;
            ld[0].LearnAimRef = "Z0007834";
            Helpers.SetLearningDeliveryEndDates(ld[0], ld[0].LearnPlanEndDate, Helpers.SetAchDate.DoNotSetAchDate);
            ld[0].AimType = (int)AimType.StandAlone;
            learner.DateOfBirth = ld[0].LearnStartDate.AddYears(-20);

            edate = ld[0].LearnActEndDate;
            if (!valid)
            {
                edate = edate.AddDays(2);
            }

            MutateWorkPlacement(learner, edate);
        }

        private void MutateWorkPlacement(MessageLearner learner, DateTime endDate)
        {
            var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = learner.LearningDelivery[0].LearnStartDate.AddDays(30),
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 1000,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 900271388,
                        WorkPlaceEndDateSpecified = true,
                        WorkPlaceEndDate = endDate
                    }
                };
                learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
           // options.LD.IncludeSOF = true;
        }
    }
}
