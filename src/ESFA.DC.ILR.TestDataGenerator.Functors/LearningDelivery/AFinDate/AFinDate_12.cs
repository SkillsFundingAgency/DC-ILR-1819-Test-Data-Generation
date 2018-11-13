using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinDate_12
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private int AppProgType;
        private string LrnAimRef;
        private int FrameworkCode;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "AFinDate_12";
        }

        public string LearnerReferenceNumberStub()
        {
            return "AfinDt12";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAdvLev, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateIntLev, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHighApp4, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHighApp5, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHighApp6, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateTrainee, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateAdvLev(MessageLearner learner, bool valid)
        {
            AppProgType = (int)ProgType.AdvancedLevelApprenticeship;
            LrnAimRef = "50104767";
            FrameworkCode = 420;
            MutateLearner(learner, valid);
        }

        private void MutateHighApp6(MessageLearner learner, bool valid)
        {
            AppProgType = (int)ProgType.HigherApprenticeshipLevel6;
            LrnAimRef = "00300356";
            FrameworkCode = 612;
            MutateLearner(learner, valid);
        }

        private void MutateHighApp5(MessageLearner learner, bool valid)
        {
            AppProgType = (int)ProgType.HigherApprenticeshipLevel5;
            LrnAimRef = "60058572";
            FrameworkCode = 487;
            MutateLearner(learner, valid);
        }

        private void MutateHighApp4(MessageLearner learner, bool valid)
        {
            AppProgType = (int)ProgType.HigherApprenticeshipLevel4;
            LrnAimRef = "50112922";
            FrameworkCode = 418;
            MutateLearner(learner, valid);
        }

        private void MutateIntLev(MessageLearner learner, bool valid)
        {
            AppProgType = (int)ProgType.IntermediateLevelApprenticeship;
            LrnAimRef = "50100312";
            FrameworkCode = 420;
            MutateLearner(learner, valid);
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
                lds.ProgType = AppProgType;
                lds.FworkCode = FrameworkCode;
            }

            if (AppProgType == 22) { Helpers.AddLearningDeliveryHE(learner); }

            aFinRec.AFinDateSpecified = true;
            aFinRec.AFinDate = ld.LearnActEndDate;
            ld.LearningDeliveryFAM[0].LearnDelFAMDateToSpecified = true;
            ld.LearningDeliveryFAM[0].LearnDelFAMDateTo = ld.LearnActEndDate;
            learner.LearningDelivery[1].LearnAimRef = LrnAimRef;
            if (!valid)
            {
                aFinRec.AFinDate = ld.LearnActEndDate.AddDays(370);
            }

            foreach (MessageLearnerLearningDelivery lds in learner.LearningDelivery)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
