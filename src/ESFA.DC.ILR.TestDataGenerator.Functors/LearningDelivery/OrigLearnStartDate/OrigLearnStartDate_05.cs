using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OrigLearnStartDate_05
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
            return "OrigLearnStartDate_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OLrnStDt05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
               //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnerAdult, DoMutateOptions = MutateGenerationOptions }
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions }
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateLearnerAdult(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            DateTime date = new DateTime(2010, 09, 01);
            MutateCommon(ld, valid, date);
        }

        private void MutateCommon(MessageLearnerLearningDelivery ld, bool valid, DateTime dt)
        {
            ld.LearnStartDate.AddYears(-19).AddMonths(-3);
            ld.OrigLearnStartDateSpecified = true;
            ld.OrigLearnStartDate = ld.LearnStartDate.AddMonths(-3);
            if (!valid) { ld.OrigLearnStartDate = dt; }
            Helpers.AddLearningDeliveryRestartFAM(ld);
        }

        private void MutateLearnerApp(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            DateTime date = new DateTime(2010, 09, 01);
            MutateCommon(ld, valid, date);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
