using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AchDate_09
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
            return "AchDate_09";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AchDt09";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateTraineeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateApprenticeshipStandard, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDate = DateTime.Parse("2015-AUG-01").AddDays(1);
                    ld.AchDateSpecified = true;
                    ld.AchDate = ld.LearnStartDate.AddDays(30);
                }
            }
        }

        private void MutateTraineeship(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                Mutate(learner, valid);
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.ProgrammeAim;
                    ld.ProgType = (int)ProgType.Traineeship;
                }
            }
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                Mutate(learner, valid);
                Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.ProgrammeAim;
                    ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
