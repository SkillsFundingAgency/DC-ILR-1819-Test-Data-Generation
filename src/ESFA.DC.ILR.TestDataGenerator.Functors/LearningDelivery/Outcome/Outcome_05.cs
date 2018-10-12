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
            foreach (var ld in learner.LearningDelivery)
            {
                ld.OutcomeSpecified = true;
                ld.Outcome = (int)Outcome.Achieved;
                if (!valid)
                {
                    ld.LearnActEndDateSpecified = false;
                }
                else
                {
                    ld.LearnActEndDateSpecified = true;
                    ld.LearnActEndDate = ld.LearnStartDate.AddDays(45);
                    ld.LearnAimRef = "60143824";
                    ld.OutGrade = "EL1";
                    ld.CompStatus = (int)CompStatus.Completed;
                    ld.LearnActEndDateSpecified = true;
                }
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
