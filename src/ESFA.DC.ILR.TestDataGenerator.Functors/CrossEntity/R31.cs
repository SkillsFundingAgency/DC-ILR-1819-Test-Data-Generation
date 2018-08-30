using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R31
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
            return "R31";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R31";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            if (!valid)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 1;
                ld.LearnActEndDateSpecified = false;
                ld.LearnStartDateSpecified = true;
                ld.LearnStartDate = DateTime.Now.AddMonths(-7);
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            if (!valid)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 1;
                ld.LearnActEndDateSpecified = true;
                ld.LearnStartDateSpecified = true;
                ld.LearnStartDate = DateTime.Now.AddMonths(-7);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
