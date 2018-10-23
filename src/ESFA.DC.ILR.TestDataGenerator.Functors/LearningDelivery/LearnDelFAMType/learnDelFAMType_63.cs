using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_63
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
            return "LearnDelFAMType_63";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy63";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateACTType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateACTTypeApprent, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateACTTypeExcl, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateACTTypeApprent(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateACTType(learner, valid);
                var ld = learner.LearningDelivery;
                ld[0].AimType = (long)AimType.ProgrammeAim;
            }
        }

        private void MutateACTTypeExcl(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateACTType(learner, valid);
                var ld = learner.LearningDelivery;
                ld[0].LearnAimRef = "60008842";
                ld[0].LearnStartDate = new DateTime(2013, 10, 14);
            }
        }

        private void MutateACTType(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.ACT, LearnDelFAMCode.ACT_ContractEmployer);
            }

            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
