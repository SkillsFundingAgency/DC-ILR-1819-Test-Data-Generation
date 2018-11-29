using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OutType_02
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
            return "OutType_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutType02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEmp },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeGap },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeNpe },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeSde },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeVol },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeOth },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEdu, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.Achieved;
            ld[0].CompStatus = (int)CompStatus.Completed;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = ld[0].LearnStartDate.AddDays(60);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.CreateDestinationAndProgression = true;
        }

        private void MutateDPOutcome(MessageLearnerDestinationandProgression learner, bool valid, OutcomeType ocType)
        {
            var dpo = learner.DPOutcome.ToList();
            dpo[0].OutType = ocType.ToString();
            if (!valid)
            {
                dpo.Add(new MessageLearnerDestinationandProgressionDPOutcome()
                {
                    OutType = ocType.ToString(),
                    OutStartDateSpecified = true,
                    OutStartDate = dpo[0].OutStartDate,
                    OutCodeSpecified = true,
                    OutCode = dpo[0].OutCode,
                    OutCollDateSpecified = true,
                    OutCollDate = dpo[0].OutCollDate
                });
            }

            learner.DPOutcome = dpo.ToArray();
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dpo = learner.DPOutcome.ToList();
            dpo[0].OutType = OutcomeType.EMP.ToString();
            if (!valid)
            {
                dpo.Add(new MessageLearnerDestinationandProgressionDPOutcome()
                {
                    OutType = OutcomeType.EMP.ToString(),
                    OutStartDateSpecified = true,
                    OutStartDate = dpo[0].OutStartDate.AddDays(1),
                    OutCodeSpecified = true,
                    OutCode = dpo[0].OutCode,
                    OutCollDateSpecified = true,
                    OutCollDate = dpo[0].OutCollDate
                });
            }

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
