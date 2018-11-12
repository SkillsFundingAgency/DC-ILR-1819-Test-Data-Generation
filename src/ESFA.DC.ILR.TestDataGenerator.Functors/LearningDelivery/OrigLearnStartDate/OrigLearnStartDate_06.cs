using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OrigLearnStartDate_06
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
            return "OrigLearnStartDate_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OLrnStDt06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerAdult, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerUnEmp, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerOlass, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnerApprent, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLearnerApprent(MessageLearner learner, bool valid)
        {
            MutateLearnerCommmon(learner, valid);
            learner.LearningDelivery[0].OrigLearnStartDate = new DateTime(2017, 5, 10);
            if (!valid) { learner.LearningDelivery[0].OrigLearnStartDate = new DateTime(2000, 10, 10); }
        }

        private void MutateLearnerOlass(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "00280267";
            MutateLearnerCommmon(learner, valid);
        }

        private void MutateLearnerUnEmp(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "00230624";
            MutateLearnerCommmon(learner, valid);
        }

        private void MutateLearnerAdult(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "00230626";
            MutateLearnerCommmon(learner, valid);
        }

        private void MutateLearnerCommmon(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            learner.LearningDelivery[0].OrigLearnStartDateSpecified = true;
            learner.LearningDelivery[0].OrigLearnStartDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(-3);
            if (valid) { learner.LearningDelivery[0].OrigLearnStartDate = new DateTime(2015, 10, 10); }
            Helpers.AddLearningDeliveryRestartFAM(learner);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
