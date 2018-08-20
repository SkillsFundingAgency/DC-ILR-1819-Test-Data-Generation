using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpStat_12
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
            return "EmpStat_12";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpStat12";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].AimType = 1;
            if (!valid)
            {
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
                learner.LearningDelivery[0].ProgType = 23;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearnerEmploymentStatus[0].EmpStatSpecified = true;
                learner.LearnerEmploymentStatus[0].EmpStat = 11;
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].AimType = 1;

            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_NonApprenticeshipSeaFishing).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.LearningDelivery[0].ProgType = 25;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearnerEmploymentStatus[0].EmpStatSpecified = true;
                learner.LearnerEmploymentStatus[0].EmpStat = 11;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
