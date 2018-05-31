using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LLDDHealthProb_07
        : ILearnerMultiMutator
    {
        private List<LLDDHealthProb> _lldd;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LLDDHealthProb_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LLDDHP_07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _lldd = new List<LLDDHealthProb>(30);
            var result = new List<LearnerTypeMutator>() {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsMultipleLD },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate25, DoMutateOptions = MutateGenerationOptionsMultipleLD, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate24WithLd3, DoMutateOptions = MutateGenerationOptionsMultipleLD },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate24WithLd3FMAdult, DoMutateOptions = MutateGenerationOptionsMultipleLDFM35, ExclusionRecord = true },
            };
            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            learner.PlanLearnHours = 11;
            if (learner.LearningDelivery[0].FundModel == (int)FundModel.NonFunded)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    Helpers.AddOrChangeLearningDeliverySourceOfFunding(ld, LearnDelFAMCode.SOF_LA);
                }
            }

            if (!valid)
            {
                learner.LLDDandHealthProblem = null;
            }
        }

        private void Mutate25(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact25, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Mutate(learner, valid);
        }

        private void Mutate24WithLd3(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact25, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[1].LearnStartDate.AddDays(-30);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less25, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Mutate(learner, valid);
        }

        private void Mutate24WithLd3FMAdult(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact25, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[1].LearnStartDate.AddDays(-30);
            learner.LearningDelivery[1].FundModel = (int)FundModel.Adult;
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less25, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Mutate(learner, valid);
            Helpers.AddOrChangeLearningDeliverySourceOfFunding(learner.LearningDelivery[1], LearnDelFAMCode.SOF_ESFA_Adult);
            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Co).ToString()
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsMultipleLD(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.LD.GenerateMultipleLDs = 3;
        }

        private void MutateGenerationOptionsMultipleLDFM35(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.LD.GenerateMultipleLDs = 3;
            options.EmploymentRequired = true;
            options.LD.IncludeHHS = true;
        }
    }
}
