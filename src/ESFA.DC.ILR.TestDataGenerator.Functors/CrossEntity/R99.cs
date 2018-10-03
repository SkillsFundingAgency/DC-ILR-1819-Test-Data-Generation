using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R99
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
            return "R99";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R99";
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
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "ZPROG001";
                    ld.LearnStartDate = new DateTime(2013, 08, 01);
                    ld.AimTypeSpecified = true;
                    ld.AimType = 1;
                    ld.ProgTypeSpecified = true;
                    ld.ProgType = 2;
                    ld.PwayCodeSpecified = true;
                    ld.PwayCode = 1;
                    ld.FworkCodeSpecified = true;
                    ld.FworkCode = 497;
                }
            }
        }

        private void MutateAppreticeship(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgTypeSpecified = false;
                //learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
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
