using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PCFLDCS_03 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PCFLDCS_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PCFLDCS03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateHE_CL, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateHE_CL(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20);
        }

        private void MutateHE(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "50066572";

            if (learner.LearningDelivery[0].FundModel == 70)
            {
                learner.LearningDelivery[0].LearnAimRef = "Z0000039";
            }

            bool SpecifiedPCFLDCS = false;
            Helpers.AddLearningDeliveryHE(learner);
            if (!valid)
            {
                SpecifiedPCFLDCS = true;
            }

            learner.LearningDelivery[0].LearningDeliveryHE[0].PCFLDCSSpecified = SpecifiedPCFLDCS;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
