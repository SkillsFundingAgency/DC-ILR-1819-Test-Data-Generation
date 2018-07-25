using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_55
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
            return "LearnAimRef_55";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR55";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnRefOne, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnRefTwo, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnRefThree, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnRefFour, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnRefFive, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnRefSix, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
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

        private void Mutate(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgTypeSpecified = true;
                ld.ProgType = (int)ProgType.Traineeship;
                ld.AimTypeSpecified = true;
                ld.AimType = (int)AimType.CoreAim1619;
                ld.LearnStartDate = new DateTime(2017, 07, 31);
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateLearnRefOne(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007834";
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateLearnRefTwo(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007835";
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateLearnRefThree(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007836";
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateLearnRefFour(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007837";
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateLearnRefFive(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "Z0007838";
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateLearnRefSix(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnAimRef = "ZWRKX001";
                ld.LearnStartDate = new DateTime(2017, 08, 01);
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.StandAlone;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
