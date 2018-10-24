using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OutType_05
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
            return "OutType_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutType05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEmp },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeOth }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery;
            ld[0].OutcomeSpecified = true;
            ld[0].Outcome = (int)Outcome.Achieved;
            ld[0].CompStatus = (int)CompStatus.Completed;
            ld[0].LearnActEndDateSpecified = true;
            ld[0].LearnActEndDate = System.DateTime.Now.AddMonths(-3);
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
            DateTime stDate = new DateTime(2015, 7, 31);

            if (!valid) { stDate = stDate.AddDays(2); }

            if (dpo[0].OutType == "EMP") { dpo[0].OutCode = 3; }

            if (dpo[0].OutType == "OTH") { dpo[0].OutCode = 2; }

            dpo[0].OutStartDate = stDate;
            string outTp;
            outTp = "SDE";
            dpo.Add(new MessageLearnerDestinationandProgressionDPOutcome()
            {
                OutType = outTp,
                OutStartDateSpecified = true,
                OutStartDate = new DateTime(2018, 7, 20),
                OutCodeSpecified = true,
                OutCode = 2,
                OutCollDateSpecified = true,
                OutCollDate = dpo[0].OutCollDate
            });
            learner.DPOutcome = dpo.ToArray();
        }

        private void MutateProgressionOutTypeEmp(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.EMP);
        }

        private void MutateProgressionOutTypeOth(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.OTH);
        }
    }
}
