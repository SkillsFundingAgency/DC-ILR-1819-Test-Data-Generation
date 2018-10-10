using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R69
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
            return "R69";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R69";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateDPOutcome, DoMutateOptions = MutateGenerationOptionsDestProg, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsDestProg, ExclusionRecord = true }
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
        }

        private void MutateDPOutcome(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery.ToList();
            lds[0].OutcomeSpecified = true;
            lds[0].Outcome = (int)Outcome.Achieved;
            lds[0].CompStatus = (int)CompStatus.Completed;
            lds[0].LearnActEndDateSpecified = true;
            lds[0].LearnActEndDate = lds[0].LearnStartDate.AddDays(45);
        }

        private void MutateGenerationOptionsDestProg(GenerationOptions options)
        {
            _options = options;
            options.EmploymentRequired = true;
            options.CreateDestinationAndProgression = true;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            if (!valid)
            {
                var dpout = learner.DPOutcome.ToList();
                dpout.Add(new MessageLearnerDestinationandProgressionDPOutcome()
                {
                    OutCode = 1,
                    OutCodeSpecified = true,
                    OutType = "VOL",
                    OutStartDateSpecified = true,
                    OutStartDate = new DateTime(2017, 11, 28),
                    OutCollDate = new DateTime(2017, 11, 30),
                    OutCollDateSpecified = true
                });

                dpout.Add(new MessageLearnerDestinationandProgressionDPOutcome()
                {
                    OutCode = 1,
                    OutCodeSpecified = true,
                    OutType = "VOL",
                    OutStartDateSpecified = true,
                    OutStartDate = new DateTime(2017, 11, 28),
                    OutCollDate = new DateTime(2017, 11, 30),
                    OutCollDateSpecified = true
                });

                learner.DPOutcome = dpout.Skip(1).ToArray();
            }
        }
    }
}