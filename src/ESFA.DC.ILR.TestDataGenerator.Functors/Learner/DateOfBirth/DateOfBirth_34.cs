using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_34
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DateOfBirth_34";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_34";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate24, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate24, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLess19, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateLess19, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate25, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true, ValidLines = 1 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate25, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true, ValidLines = 1 },
            };
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, Helpers.AgeRequired.Exact19);
        }

        private void MutateLess19(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, Helpers.AgeRequired.Less19);
        }

        private void Mutate24(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, Helpers.AgeRequired.Less25);
        }

        private void Mutate25(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid, Helpers.AgeRequired.Exact25);
        }

        private void MutateCommon(MessageLearner learner, bool valid, Helpers.AgeRequired age)
        {
            Helpers.MutateDOB(learner, valid, age, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            foreach (var ld in learner.LearningDelivery)
            {
                Helpers.AddOrChangeLearningDeliverySourceOfFunding(ld, LearnDelFAMCode.SOF_ESFA_1619);
            }

            if (!valid)
            {
                var fams = learner.LearnerFAM.Where(s => s.LearnFAMType != LearnerFAMType.EHC.ToString()).ToList();
                learner.LearnerFAM = fams.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.FAM.EducationHealthCarePlanRequired = true;
            options.FAM.HighNeedsStudentRequired = true;
        }
    }
}
