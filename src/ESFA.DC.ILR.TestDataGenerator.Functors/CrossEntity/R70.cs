using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R70
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
            return "R70";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R70";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateWithStd, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            int aim;
            learner.LearningDelivery[0].AimType = 3;
            learner.LearningDelivery[0].ProgType = 25;
            learner.LearningDelivery[1].ProgType = 25;
            aim = valid ? 1 : 2;
            learner.LearningDelivery[1].AimType = aim;
        }

        private void MutateWithStd(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.StdCodeSpecified = true;
                ld.StdCode = 5;
            }

            MutateCommon(learner, valid);
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
