using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_78
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
            return "LearnAimRef_78";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR78";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            //var ld = learner.LearningDelivery;
            //ld[0].LearnStartDate = new DateTime(2018, 8, 1);
            //if (!valid)
            //{
            //    ld[0].LearnAimRef = "10034055";
            //    ld[0].LearnStartDate = new DateTime(2016, 8, 31);
            //}
            learner.LLDDHealthProb = 1;
            learner.LLDDHealthProbSpecified = true;
            //learner.LLDDandHealthProblem[0].LLDDCat = 4;
            //learner.LLDDandHealthProblem[0].PrimaryLLDD = 1;
       }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsCL(GenerationOptions options)
        {
            //options.LD.IncludeSOF = true;
            //options.LLDDHealthProblemRequired = true;
        }
    }
}
