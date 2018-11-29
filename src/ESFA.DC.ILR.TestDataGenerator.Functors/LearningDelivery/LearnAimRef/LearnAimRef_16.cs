using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_16
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LearnAimRef_16";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR16";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateValidity, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnerApp, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppreticeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateValidity, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "50104767";
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 10, 14);
                learner.LearningDelivery[0].AimTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 3;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = 2;
                learner.LearningDelivery[0].PwayCodeSpecified = true;
                learner.LearningDelivery[0].PwayCode = 1;
                learner.LearningDelivery[0].FworkCodeSpecified = true;
                learner.LearningDelivery[0].FworkCode = 420;
            }
        }

        private void MutateLearnerApp(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateValidity(learner, valid);
            }
        }

        private void MutateValidity(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "50072985";
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2013, 08, 01);
                learner.LearningDelivery[0].AimTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 3;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = 2;
                learner.LearningDelivery[0].PwayCodeSpecified = true;
                learner.LearningDelivery[0].PwayCode = 1;
                learner.LearningDelivery[0].FworkCodeSpecified = true;
                learner.LearningDelivery[0].FworkCode = 497;
            }
        }

        private void MutateAppreticeship(MessageLearner learner, bool valid)
        {
            MutateValidity(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
            }
        }

        private void MutateRestarts(MessageLearner learner, bool valid)
        {
            MutateValidity(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
