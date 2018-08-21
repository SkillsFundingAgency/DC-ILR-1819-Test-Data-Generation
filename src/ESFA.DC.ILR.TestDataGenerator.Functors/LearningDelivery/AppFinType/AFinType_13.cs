using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_13
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
            return "AFinType_13";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty13";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAfinDate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAfinDate1, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateAimType, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
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

            if (!valid)
            {
                    var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                    appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                    {
                        AFinAmount = 500,
                        AFinAmountSpecified = true,
                        AFinType = LearnDelAppFinType.PMR.ToString(),
                        AFinCode = (int)LearnDelAppFinCode.TrainingPayment,
                        AFinCodeSpecified = true,
                        AFinDate = ld.LearnStartDate,
                        AFinDateSpecified = true
                    });

                    ld.AppFinRecord = appfin.ToArray();
            }
        }

        private void MutateAfinDate(MessageLearner learner, bool valid)
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

            if (!valid)
            {
                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TrainingPayment,
                    AFinCodeSpecified = true,
                    AFinDate = ld.LearnStartDate.AddDays(1),
                    AFinDateSpecified = true
                });

                ld.AppFinRecord = appfin.ToArray();
            }
        }

        private void MutateAfinDate1(MessageLearner learner, bool valid)
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

            if (!valid)
            {
                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TrainingPayment,
                    AFinCodeSpecified = true,
                    AFinDate = ld.LearnStartDate.AddDays(-1),
                    AFinDateSpecified = true
                });

                ld.AppFinRecord = appfin.ToArray();
            }
        }

        private void MutateAimType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateLearner(learner, valid);
                ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
                learner.LearningDelivery[0].ProgType = (int)pta.ProgType;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = (int)AimType.ProgrammeAim;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
