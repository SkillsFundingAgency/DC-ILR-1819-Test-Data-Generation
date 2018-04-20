using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ULN_10
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.January;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHEFCE, DoMutateOptions = MutateGenerationOptionsHEFCE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHEFCEOLASS, DoMutateOptions = MutateGenerationOptionsHEFCE, ValidLines = 1, ExclusionRecord = true }
            };
        }

        public void MutateHEFCEOLASS(MessageLearner learner, bool valid)
        {
            MutateHEFCE(learner, valid);
            Helpers.MutateLearningDeliveryMonitoringLDMToNewCode(learner, LearnDelFAMCode.LDM_OLASS);
        }

        public void MutateHEFCE(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString();
            learner.ULN = 9999999999;
            learner.ULNSpecified = true;
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|DEC|13]"));
            if (valid)
            {
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(3);
            }
            else
            {
                learner.LearningDelivery[0].LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate + TimeSpan.FromDays(5);
            }
        }

        public void MutateGenerationOptionsHEFCE(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeSOF = true;
            options.LD.IncludeHEFields = true;
            options.LD.IncludeLDM = true;
        }

        public string RuleName()
        {
            return "ULN_10";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ULN_10";
        }
    }
}
