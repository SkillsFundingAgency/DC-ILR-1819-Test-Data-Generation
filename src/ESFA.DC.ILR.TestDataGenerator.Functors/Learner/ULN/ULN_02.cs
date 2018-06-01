using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ULN_02 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsSOF },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHEFCE, DoMutateOptions = MutateGenerationOptionsHEFCE, ExclusionRecord = true }
            };
        }

        public void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.ULN = 9999999999;
                learner.ULNSpecified = true;
            }
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        public void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }

        public void MutateGenerationOptionsHEFCE(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
            options.LD.IncludeHEFields = true;
            options.EmploymentRequired = true;
        }

        public void MutateHEFCE(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString();
            learner.ULN = 9999999999;
            learner.ULNSpecified = true;
            learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(3);
        }

        public string RuleName()
        {
            return "ULN_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ULN_02";
        }
    }
}
