using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class WorkPlaceEmpId_05
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
            return "WorkPlaceEmpId_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "WrkPlcEId05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "Z0007834";
            if (valid)
            {
                var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = new DateTime(2017, 08, 01),
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 1000,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 900271388
                    }
                };

                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgTypeSpecified = true;
                    ld.ProgType = (int)ProgType.Traineeship;
                    ld.AimTypeSpecified = true;
                    ld.AimType = (int)AimType.CoreAim1619;
                    ld.LearnStartDate = new DateTime(2017, 07, 31);
                }

                learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
            }

            if (!valid)
            {
                var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = new DateTime(2017, 08, 01),
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 1000,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = false
                    }
                };

                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgTypeSpecified = true;
                    ld.ProgType = (int)ProgType.Traineeship;
                    ld.AimTypeSpecified = true;
                    ld.AimType = (int)AimType.CoreAim1619;
                    ld.LearnStartDate = new DateTime(2017, 07, 31);
                }

                learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = DateTime.Now.AddDays(-59),
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 1000,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = false
                    }
                };

                learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
