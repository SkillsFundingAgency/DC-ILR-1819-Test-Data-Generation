using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpID_02
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
            return "EmpID_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpID02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLES, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLES(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateCommon(learner, valid);
            }

            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = DateTime.Now.AddMonths(-2);
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpIdSpecified = true;
                les.EmpId = 99999999;
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
             if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = DateTime.Now.AddMonths(-2);
                var les = learner.LearnerEmploymentStatus[0];
                les.EmpIdSpecified = true;
                les.EmpId = 999999999;
            }
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var ld = learner.LearningDelivery[0];
            if (valid)
            {
                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TotalTrainingPrice,
                    AFinCodeSpecified = true,
                    AFinDate = ld.LearnStartDate,
                    AFinDateSpecified = true
                });

                ld.AppFinRecord = appfin.ToArray();
            }
        }

        private void MutateLDMType(MessageLearner learner, bool valid)
        {
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
                var les = learner.LearnerEmploymentStatus[0];
                les.DateEmpStatAppSpecified = true;
                les.DateEmpStatApp = new DateTime(2017, 12, 01);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
