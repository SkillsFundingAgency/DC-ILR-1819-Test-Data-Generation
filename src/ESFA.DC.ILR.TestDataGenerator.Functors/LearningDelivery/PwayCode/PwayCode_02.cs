using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PwayCode_02
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
            return "PwayCode_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PwayCd02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTraineeship, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
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
                ld.LearnAimRef = "Z0007834";
                ld.ProgTypeSpecified = true;
                ld.ProgType = (int)ProgType.Traineeship;
                ld.AimTypeSpecified = true;
                ld.AimType = (int)AimType.CoreAim1619;
                ld.LearnStartDate = new DateTime(2017, 07, 31);
            }

            learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.PwayCodeSpecified = true;
                    ld.PwayCode = 420;
                }
            }
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.NonFunded);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.SetApprenticeshipAims(learner, pta);
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                ld.AppFinRecord = null;
                ld.EPAOrgID = null;
                Helpers.AddOrChangeLearningDeliverySourceOfFunding(ld, LearnDelFAMCode.SOF_Other);
            }

            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            if (!valid)
            {
                learner.LearningDelivery[0].PwayCodeSpecified = true;
                learner.LearningDelivery[0].PwayCode = 420;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
