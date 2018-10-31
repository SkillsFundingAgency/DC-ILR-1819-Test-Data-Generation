using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R56
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
            return "R56";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R56";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateValidity, DoMutateOptions = MutateGenerateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppreticeship, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                var lds = learner.LearningDelivery.ToList();
                learner.LearningDelivery[1].LearnAimRef = "50104767";
                learner.LearningDelivery[1].LearnStartDate = new DateTime(2017, 10, 14);
                learner.LearningDelivery[1].AimTypeSpecified = true;
                learner.LearningDelivery[1].AimType = 3;
                learner.LearningDelivery[1].ProgTypeSpecified = true;
                learner.LearningDelivery[1].ProgType = 2;
                learner.LearningDelivery[1].PwayCodeSpecified = true;
                learner.LearningDelivery[1].PwayCode = 1;
                learner.LearningDelivery[1].FworkCodeSpecified = true;
                learner.LearningDelivery[1].FworkCode = 420;
            }
        }

        private void MutateValidity(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].FundModelSpecified = true;
                learner.LearningDelivery[0].FundModel = 99;
                learner.LearningDelivery[1].LearnAimRef = "50104767";
                learner.LearningDelivery[1].LearnStartDate = new DateTime(2017, 10, 14);
                learner.LearningDelivery[1].AimTypeSpecified = true;
                learner.LearningDelivery[1].AimType = 3;
                learner.LearningDelivery[1].ProgTypeSpecified = true;
                learner.LearningDelivery[1].ProgType = 2;
                learner.LearningDelivery[1].PwayCodeSpecified = true;
                learner.LearningDelivery[1].PwayCode = 1;
                learner.LearningDelivery[1].FworkCodeSpecified = true;
                learner.LearningDelivery[1].FworkCode = 420;
                learner.LearningDelivery[1].FundModelSpecified = true;
                learner.LearningDelivery[1].FundModel = 36;
            }
        }

        private void MutateAppreticeship(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].FundModelSpecified = true;
                learner.LearningDelivery[0].FundModel = 99;
                learner.LearningDelivery[1].FundModelSpecified = true;
                learner.LearningDelivery[1].FundModel = 99;
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.SOF);
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            }
        }

        private void MutateOptions(GenerationOptions options)
        {
        }

        private void MutateGenerateOptions(GenerationOptions options)
        {
        }
    }
}
