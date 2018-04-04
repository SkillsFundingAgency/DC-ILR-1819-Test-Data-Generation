using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class GivenNames_04
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "GIVNAM_04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateSOFLA, DoMutateOptions = MutateGenerationOptionsSOF },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsSOF },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateSOFLAMLD, DoMutateOptions = MutateGenerationOptionsSOFMLD, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateMLD, DoMutateOptions = MutateGenerationOptionsSOFMLD, ExclusionRecord = true },
            };
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
            options.EmploymentRequired = true;
        }

        private void MutateGenerationOptionsSOFMLD(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeSOF = true;
            options.LD.GenerateMultipleLDs = 3;
            options.LD.IncludeHHS = true;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.PlanLearnHours = 10;
            if (!valid)
            {
                learner.GivenNames = null; // string.Empty will actually fire a FD rule
            }
        }

        private void MutateSOFLA(MessageLearner learner, bool valid)
        {
            foreach (var v in learner.LearningDelivery)
            {
                Helpers.AddOrChangeSourceOfFunding(v, LearnDelFAMCode.SOF_LA);
            }

            Mutate(learner, valid);
        }

        private void MutateMLD(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[1];
            ld.FundModel = (int)FundModel.Adult;
            var ldFams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ASL.ToString()).ToList();
            ldFams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString()
            });
            ld.LearningDeliveryFAM = ldFams.ToArray();
            Mutate(learner, valid);
        }

        private void MutateSOFLAMLD(MessageLearner learner, bool valid)
        {
            MutateMLD(learner, valid);
            foreach (var v in learner.LearningDelivery)
            {
                if (v.FundModel == (int)FundModel.NonFunded)
                {
                    Helpers.AddOrChangeSourceOfFunding(v, LearnDelFAMCode.SOF_LA);
                }
            }
        }
    }
}
