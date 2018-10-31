using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FD_AimType_MA
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateAImType1, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public string RuleName()
        {
            return "FD_AimType_MA";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FDAimTy";
        }

        private void MutateAImType1(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (true)
            {
                learner.LearningDelivery[0].AimTypeSpecified = false;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
