using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Outcome_08
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
            return "Outcome_08";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutCom08";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateOutcome, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].OutcomeSpecified = true;
            lds[0].Outcome = (int)Outcome.Achieved;
            lds[0].LearnAimRef = "60143824";
            lds[0].OutGrade = "EL1";
            lds[0].CompStatus = (int)CompStatus.Completed;
            lds[0].LearnActEndDateSpecified = true;
            lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(45);
            if (!valid)
            {
                lds[0].OutGrade = "SU";
            }
        }

        private void MutateOutcome(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].OutcomeSpecified = true;
            lds[0].Outcome = (int)Outcome.Achieved;
            lds[0].CompStatus = (int)CompStatus.Completed;
            lds[0].LearnActEndDateSpecified = true;
            lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(45);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "ZPROG001";
                learner.LearningDelivery[0].AimTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = (int)ProgType.Traineeship;
            }
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.CreateDestinationAndProgression = true;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dpo = learner.DPOutcome[0];
            dpo.OutCollDate = dpo.OutStartDate.AddDays(45);
        }
    }
}
