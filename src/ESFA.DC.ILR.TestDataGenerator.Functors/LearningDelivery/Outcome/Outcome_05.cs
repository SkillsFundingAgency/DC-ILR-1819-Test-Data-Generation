using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Outcome_05
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
            return "Outcome_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutCom05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].OutcomeSpecified = true;
            lds[0].Outcome = (int)Outcome.Achieved;
            if (!valid)
            {
                lds[0].LearnActEndDateSpecified = false;
            }
            else
            {
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(45);
                lds[0].LearnAimRef = "60143824";
                lds[0].OutGrade = "EL1";
                lds[0].CompStatus = (int)CompStatus.Completed;
                lds[0].LearnActEndDateSpecified = true;
            }
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            var dpo = learner.DPOutcome[0];
            dpo.OutCollDate = dpo.OutStartDate.AddDays(45);
        }
    }
}
