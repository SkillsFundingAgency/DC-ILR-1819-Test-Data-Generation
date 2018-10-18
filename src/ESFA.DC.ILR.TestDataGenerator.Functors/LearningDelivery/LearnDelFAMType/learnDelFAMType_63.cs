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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateACTType, DoMutateOptions = MutateGenerationOptions }
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateACTType, DoMutateOptions = MutateGenerationOptions }
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAIM3Type, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateACTType(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].AimType = (long)AimType.ProgrammeAim;
            ld[0].LearningDeliveryFAM[0].LearnDelFAMCode = "1";
            ld[0].LearningDeliveryFAM[0].LearnDelFAMType = LearnDelFAMType.ACT.ToString();
            if (!valid)
            {
                ld[0].FundModel = (long)LearnerTypeRequired.YP1619;
                ld[0].AimType = (long)AimType.CoreAim1619;
            }

            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }

           // RemoveLD(learner, 1);
        }

        private void RemoveLD(MessageLearner learner, int index)
        {
            var ld = learner.LearningDelivery.ToList();
            ld.Remove(ld[index]);
            learner.LearningDelivery = ld.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
