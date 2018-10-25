using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_48
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
            return "LearnDelFAMType_48";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy48";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHHS, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateHHS(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HHS, LearnDelFAMCode.HHS_SingleWithChildren);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HHS, LearnDelFAMCode.HHS_NoEmploymentNoChildren);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHHS = true;
        }
    }
}
