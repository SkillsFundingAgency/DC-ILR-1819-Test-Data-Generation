using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnStartDate_12
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
            return "LearnStartDate_12";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LstartDt12";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var year = DateTime.Now.Year;
                if (DateTime.Now.Month > 7)
                {
                    year = year + 2;
                }
                else
                {
                    year = year + 1;
                }

                learner.LearningDelivery[0].LearnStartDate = new DateTime(year, 08, 01);
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var year = DateTime.Now.Year;
                if (DateTime.Now.Month > 7)
                {
                    year = year + 1;
                }

                learner.LearningDelivery[0].LearnStartDate = new DateTime(year, 07, 31);
            }
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var year = DateTime.Now.Year;
                if (DateTime.Now.Month > 7)
                {
                    year = year + 1;
                }

                learner.LearningDelivery[0].LearnStartDate = new DateTime(year, 08, 01);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
