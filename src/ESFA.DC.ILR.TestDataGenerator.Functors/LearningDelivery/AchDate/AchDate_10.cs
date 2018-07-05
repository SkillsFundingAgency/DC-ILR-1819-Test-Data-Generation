using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AchDate_10
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
            return "AchDate_10";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AchDt10";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.AimType = (int)AimType.ProgrammeAim;
                    ld.ProgType = (int)ProgType.Traineeship;
                    Helpers.SetLearningDeliveryEndDates(ld, ld.LearnStartDate.AddDays(30), Helpers.SetAchDate.SetAchDate);
                    ld.AchDate = ld.LearnActEndDate.AddDays(1).AddMonths(6);
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }
    }
}
