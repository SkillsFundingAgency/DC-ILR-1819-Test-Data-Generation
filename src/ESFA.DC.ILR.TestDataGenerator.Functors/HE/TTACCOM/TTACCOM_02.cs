using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class TTACCOM_02
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "TTACCOM_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "TTACC02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var lhe = new List<MessageLearnerLearnerHE>();

            ld.LearnAimRef = "50036063";
            ld.LearnStartDate = new DateTime(2008, 07, 31).AddDays(-1);
            ld.LearnPlanEndDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(6);

            lhe.Add(new MessageLearnerLearnerHE()
            {
                TTACCOMSpecified = true,
                TTACCOM = 3
            });

            learner.LearnerHE = lhe.ToArray();

            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2008, 07, 31).AddDays(1);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
        }
    }
}
