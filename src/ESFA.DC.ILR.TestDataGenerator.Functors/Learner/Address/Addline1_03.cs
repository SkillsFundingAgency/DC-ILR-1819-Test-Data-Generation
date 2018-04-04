using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Addline1_03 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "Addl1_03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsCL },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateShortPlannedHours, DoMutateOptions = MutateGenerationOptionsCL, InvalidLines = 0, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateShortPlannedHoursWithSOF, DoMutateOptions = MutateGenerationOptionsNF, InvalidLines = 0, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateWithSOF, DoMutateOptions = MutateGenerationOptionsNF }
            };
        }

        public void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.AddLine1 = null;
            }
        }

        public void MutateShortPlannedHoursWithSOF(MessageLearner learner, bool valid)
        {
            MutateShortPlannedHours(learner, valid);
            SetSOF(learner);
        }

        public void MutateWithSOF(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            SetSOF(learner);
        }

        private void SetSOF(MessageLearner learner)
        {
            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.SOF.ToString()).First();
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_LA).ToString();
        }

        private void MutateShortPlannedHours(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            learner.PlanLearnHours = 10;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsCL(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsNF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
            options.EmploymentRequired = true;
        }
    }
}
