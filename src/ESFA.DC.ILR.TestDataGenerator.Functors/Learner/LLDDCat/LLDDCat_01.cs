using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LLDDCat_01
        : ILearnerMultiMutator
    {
        private List<LLDDCat> _lldd;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LLDDCat_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LLDDC_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _lldd = new List<LLDDCat>(30);
            var result = new List<LearnerTypeMutator>();

            foreach (var eth in Enum.GetValues(typeof(LLDDCat)))
            {
                _lldd.Add((LLDDCat)eth);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            for (int i = 0; i != 3; ++i)
            {
                result[i].DoMutateOptions = MutateGenerationOptionsStartDate;
            }

            result[0].LearnerType = LearnerTypeRequired.Adult;
            result[1].LearnerType = LearnerTypeRequired.OtherYP1619;

            result[3].LearnerType = LearnerTypeRequired.Apprenticeships;

            result[4].LearnerType = LearnerTypeRequired.NonFunded;
            result[4].DoMutateOptions = MutateGenerationOptionsEmployment;

            result[5].LearnerType = LearnerTypeRequired.CommunityLearning;
            result[5].DoMutateOptions = MutateGenerationOptionsSOF;

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LLDDandHealthProblem[0].LLDDCat = (int)_lldd[0];
            learner.LLDDandHealthProblem[0].LLDDCatSpecified = true;
            learner.LLDDHealthProb = (int)LLDDHealthProb.LearningDifficultyOrHealthProblem;
            if (!valid)
            {
                if (learner.LLDDandHealthProblem[0].LLDDCat > 20)
                {
                    learner.LLDDandHealthProblem[0].LLDDCat -= 20;
                }
                else
                {
                    learner.LLDDandHealthProblem[0].LLDDCat += 20;
                }
            }

            _lldd.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LLDDHealthProblemRequired = true;
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsStartDate(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.LD.OverrideLearnStartDate = DateTime.Parse("2015-JUL-30");
        }

        private void MutateGenerationOptionsEmployment(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.EmploymentRequired = true;
        }
    }
}
