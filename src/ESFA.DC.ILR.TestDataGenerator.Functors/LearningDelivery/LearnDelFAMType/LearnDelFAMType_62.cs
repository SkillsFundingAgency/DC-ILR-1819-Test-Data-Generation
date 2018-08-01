using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_62
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
            return "LearnDelFAMType_62";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy62";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateFFI, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTraineeType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRoTLType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateFFI(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
                learner.LearningDelivery[0].ProgType = 23;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateTraineeType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60325999";
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_OLASS).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateRoTLType(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 12, 01);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-20).AddMonths(-3);
            if (!valid)
            {
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_RoTL).ToString(),
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = 9;
                learner.LearningDelivery[0].LearnAimRef = "60326001";
                learner.LearningDelivery[0].ProgType = 24;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = 1;
            }
        }

        private void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    var ld0Fams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.LDM.ToString());
                    ld.LearningDeliveryFAM = ld0Fams.ToArray();
                }

                learner.LearningDelivery[0].LearnStartDate = new DateTime(2013, 08, 01).AddDays(-1);
            }
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            var led = learner.LearningDelivery[0];
            var ldfams = led.LearningDeliveryFAM.ToList();
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
            });

            led.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
