using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OrigLearnStartDate_04
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
            return "OrigLearnStartDate_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OLrnStDt04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateFundAdj, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateNF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateCL, DoMutateOptions = MutateGenerationOptionsSOF, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            learner.LearningDelivery[0].OrigLearnStartDateSpecified = true;
            learner.LearningDelivery[0].OrigLearnStartDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryRestartFAM(learner);
            }
        }

        private void MutateFundAdj(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            foreach (var lds in learner.LearningDelivery)
            {
                lds.PriorLearnFundAdjSpecified = true;
                lds.PriorLearnFundAdj = 99;
            }
        }

        private void MutateNF(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-18).AddMonths(-3);
        }

        private void MutateCL(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20);
            if (!valid)
            {
                learner.LearningDelivery[0].OrigLearnStartDate = learner.LearningDelivery[0].LearnStartDate.AddDays(1);
                learner.LearningDelivery[0].OrigLearnStartDateSpecified = true;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
