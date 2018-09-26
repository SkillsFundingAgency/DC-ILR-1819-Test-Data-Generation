using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ESMType_11
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
            return "ESMType_11";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ESMType11";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLES, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = MutateOther, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = 10;
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.RemoveRange(0, 1);
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ASL.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ASL_Personal).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
            }

            if (!valid)
            {
                MutateInvalid(learner, valid);
            }
        }

        private void MutateOther(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = 10;
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
            }

            if (!valid)
            {
                MutateInvalid(learner, valid);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.RemoveRange(0, 1);
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateOther(learner, valid);
            }

            if (!valid)
            {
                //MutateInvalid(learner, valid);
                //var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                //ld1Fams.RemoveRange(0, 1);
                //var les = learner.LearnerEmploymentStatus[0];
                //var lesm = les.EmploymentStatusMonitoring.ToList();
                //lesm.RemoveRange(0, 1);
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpStatSpecified = true;
                les.EmpStat = 10;
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-2);
            }
        }

        private void MutateInvalid(MessageLearner learner, bool valid)
        {
                var les = learner.LearnerEmploymentStatus[0];
                var lesm = les.EmploymentStatusMonitoring.ToList();
                lesm.RemoveRange(0, 2);
                lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                {
                    ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                    ESMCode = (int)EmploymentStatusMonitoringCode.Employed12Plus,
                    ESMCodeSpecified = true
                });

                learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = new DateTime(2018, 09, 30);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
