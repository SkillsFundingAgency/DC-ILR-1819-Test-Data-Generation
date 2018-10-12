using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class OutType_03
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
            return "OutType_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "OutType03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeEdu },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgressionOutTypeNpe }
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
            //DateTime stDate = dpo[0].OutStartDate.AddMonths(1);
            //if (!valid)
            //{
            //    stDate = dpo[0].OutStartDate;
            //}

            //string outTp;
            //outTp = ocType.ToString().Equals("EMP") ? "NPE" : "EMP";
            int i = 2;
            string oType = "EDU";
            while (i > 0)
            {
                if (i == 1) { oType = "EDU  "; }
                dpo.Add(new MessageLearnerDestinationandProgressionDPOutcome()
                {
                    OutType = oType,
                    OutStartDateSpecified = true,
                    OutStartDate = dpo[0].OutStartDate,
                    OutCodeSpecified = true,
                    OutCode = dpo[0].OutCode,
                    OutCollDateSpecified = true,
                    OutCollDate = dpo[0].OutCollDate
                });
                learner.DPOutcome = dpo.ToArray();
                --i;
            }
        }

        private void MutateProgressionOutTypeEdu(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.EDU);
        }

        private void MutateProgressionOutTypeNpe(MessageLearnerDestinationandProgression learner, bool valid)
        {
            MutateDPOutcome(learner, valid, OutcomeType.NPE);
        }
    }
}
