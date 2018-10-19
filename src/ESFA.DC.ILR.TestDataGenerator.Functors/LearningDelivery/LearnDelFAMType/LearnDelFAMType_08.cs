using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_08
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
            return "LearnDelFAMType_08";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy08";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnerFAMSFA, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnerFAMEFA, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateLearnerFAMEFA(MessageLearner learner, bool valid)
        {
            LearnDelFAMCode Lcode;
            Lcode = (!valid) ? LearnDelFAMCode.SOF_ESFA_Adult : LearnDelFAMCode.SOF_LA;
            MutateLearnerCommon(learner, Lcode);
        }

        private void MutateLearnerFAMSFA(MessageLearner learner, bool valid)
        {
            LearnDelFAMCode Lcode;
            Lcode = (!valid) ? LearnDelFAMCode.SOF_ESFA_1619 : LearnDelFAMCode.SOF_Other;
            MutateLearnerCommon(learner, Lcode);
        }

        private void MutateLearnerCommon(MessageLearner learner, LearnDelFAMCode code)
        {
            Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.SOF, code);
            var lds = learner.LearningDelivery;
            foreach (MessageLearnerLearningDelivery del in lds)
            {
                del.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
