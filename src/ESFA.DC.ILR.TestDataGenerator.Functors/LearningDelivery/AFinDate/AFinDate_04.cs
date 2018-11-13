using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinDate_04
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
            return "AFinDate_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AfinDt04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            Helpers.AddAfninRecord(learner, LearnDelAppFinType.TNP.ToString(), (int)LearnDelAppFinCode.TotalTrainingPrice, 500);
            var aFinRec = learner.LearningDelivery[0].AppFinRecord[0];
            var ld = learner.LearningDelivery[0];
            foreach (var lds in learner.LearningDelivery)
            {
                lds.LearnActEndDateSpecified = true;
                lds.LearnActEndDate = ld.LearnStartDate.AddMonths(12);
                lds.CompStatusSpecified = true;
                lds.CompStatus = (int)CompStatus.Completed;
                lds.OutcomeSpecified = true;
                lds.Outcome = (int)Outcome.Achieved;
            }

            aFinRec.AFinDateSpecified = true;
            aFinRec.AFinDate = ld.LearnActEndDate;

            ld.LearningDeliveryFAM[0].LearnDelFAMDateToSpecified = true;
            ld.LearningDeliveryFAM[0].LearnDelFAMDateTo = ld.LearnActEndDate;
            learner.LearningDelivery[1].LearnAimRef = "50104767";

            if (!valid)
            {
                aFinRec.AFinDate = ld.LearnActEndDate.AddDays(1);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
