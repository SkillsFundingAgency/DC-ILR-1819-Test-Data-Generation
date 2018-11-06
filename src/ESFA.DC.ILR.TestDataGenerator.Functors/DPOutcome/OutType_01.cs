using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OutType_01
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
            return "OutType_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutType01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEmp },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeGap },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnerApp, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeNpe },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearnerCL, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeSde },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeVol },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnerApp, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeOth },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEdu },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEmp }
            };
        }

        private void MutateLearnerCL(MessageLearner learner, bool valid)
        {
           learner.LearningDelivery[0].LearnAimRef = "00260973";
           learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20);
        }

        private void MutateLearnerApp(MessageLearner learner, bool valid)
        {
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.Achieved;
            ld[0].CompStatus = (int)CompStatus.Completed;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddDays(60);
            ld[0].LearningDeliveryFAM[0].LearnDelFAMDateFrom = ld[0].LearnActEndDate;

            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.CreateDestinationAndProgression = true;
        }

        private void MutateDPOutcome(MessageLearnerDestinationandProgression learner, bool valid, OutcomeType ocType)
        {
            OutcomeType outcomeTp;
            outcomeTp = (!valid) ? OutcomeType.INV : ocType;
            var dpo = learner.DPOutcome.ToList();
            dpo[0].OutType = outcomeTp.ToString();
            learner.DPOutcome = dpo.ToArray();
        }

        private void MutateProgressionOutTypeEmp(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.EMP);
        }

        private void MutateProgressionOutTypeNpe(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.NPE);
        }

        private void MutateProgressionOutTypeVol(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.VOL);
        }

        private void MutateProgressionOutTypeGap(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.GAP);
        }

        private void MutateProgressionOutTypeSde(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.SDE);
        }

        private void MutateProgressionOutTypeOth(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.OTH);
        }

        private void MutateProgressionOutTypeEdu(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.EDU);
        }
    }
}
