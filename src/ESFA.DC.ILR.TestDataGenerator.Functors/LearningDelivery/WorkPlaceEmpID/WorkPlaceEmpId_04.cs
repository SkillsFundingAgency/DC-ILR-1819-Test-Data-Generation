using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class WorkPlaceEmpId_04
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
            return "WorkPlaceEmpId_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "WrkPlcEId04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateSTartDateOne, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateSTartDateTwo, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, DateTime stdate, long wpei)
        {
            var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
            {
                new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                {
                    WorkPlaceStartDateSpecified = true,
                    WorkPlaceStartDate = stdate,
                    WorkPlaceHoursSpecified = true,
                    WorkPlaceHours = 1000,
                    WorkPlaceModeSpecified = true,
                    WorkPlaceMode = 1,
                    WorkPlaceEmpIdSpecified = true,
                    WorkPlaceEmpId = wpei
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

        private void MutateSTartDateOne(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007834";
            }

            if (valid)
            {
                MutateLearner(learner, new DateTime(2017, 08, 01), 900271388);
            }

            if (!valid)
            {
                MutateLearner(learner, DateTime.Now.AddDays(-61), 999999999);
            }
        }

        private void MutateSTartDateTwo(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007834";
            }

            if (valid)
            {
                MutateLearner(learner, new DateTime(2017, 08, 01), 900271388);
            }

            if (!valid)
            {
                MutateLearner(learner, DateTime.Now.AddDays(-59), 999999999);
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            // MutateSTartDateOne(learner, valid);
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
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 999999999
                    }
                };
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
