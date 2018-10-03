using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R110
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
            return "R110";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R110";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.LearnerEmploymentStatus[0].EmpStatSpecified = true;
            if (valid)
            {
                learner.LearnerEmploymentStatus[0].EmpStat = 10;
            }

            if (!valid)
            {
                learner.LearnerEmploymentStatus[0].EmpStat = 11;
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgTypeSpecified = false;
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                var ldfams = ld.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_NonApprenticeshipSeaFishing).ToString(),
                });

                ld.LearningDeliveryFAM = ldfams.ToArray();
                learner.LearnerEmploymentStatus[0].EmpStatSpecified = true;
                learner.LearnerEmploymentStatus[0].EmpStat = 11;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
