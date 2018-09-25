using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R112
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
            return "R112";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R112";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateCommon, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateCommon, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateCommon, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateProgType, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateTrainee, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            MutateValid(learner, valid);

            if (!valid)
            {
                MutateInvalid(learner, valid);
            }
        }

        private void MutateInvalid(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDateSpecified = true;
                ld.LearnStartDate = new DateTime(2017, 08, 01);
                ld.LearnActEndDateSpecified = true;
                ld.LearnActEndDate = DateTime.Now.AddMonths(-1);
                ld.CompStatusSpecified = true;
                ld.CompStatus = (int)CompStatus.Completed;
            }

            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
                       var led = learner.LearningDelivery[0];
            var ldfams = led.LearningDeliveryFAM.ToList();
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                LearnDelFAMDateFromSpecified = true,
                LearnDelFAMDateFrom = DateTime.Now.AddMonths(-4),
                LearnDelFAMDateToSpecified = true,
                LearnDelFAMDateTo = DateTime.Now.AddMonths(-2)
            });

            led.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateValid(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDateSpecified = true;
                ld.LearnStartDate = new DateTime(2017, 08, 01);
                ld.LearnActEndDateSpecified = true;
                ld.LearnActEndDate = DateTime.Now.AddMonths(-1);
                ld.CompStatusSpecified = true;
                ld.CompStatus = (int)CompStatus.Completed;
                ld.OutcomeSpecified = true;
                ld.Outcome = (int)Outcome.Achieved;
            }

            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
            var led = learner.LearningDelivery[0];
            var ldfams = led.LearningDeliveryFAM.ToList();
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                LearnDelFAMCode = "1",
                LearnDelFAMDateFromSpecified = true,
                LearnDelFAMDateFrom = led.LearnStartDate,
                LearnDelFAMDateToSpecified = true,
                LearnDelFAMDateTo = DateTime.Now.AddMonths(-1)
            });

            led.LearningDeliveryFAM = ldfams.ToArray();
            var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
            appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = 500,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.TNP.ToString(),
                AFinCode = (int)LearnDelAppFinCode.TotalTrainingPrice,
                AFinCodeSpecified = true,
                AFinDate = led.LearnStartDate,
                AFinDateSpecified = true
            });

            led.AppFinRecord = appfin.ToArray();
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDateSpecified = true;
                    ld.LearnStartDate = new DateTime(2017, 08, 01);
                    ld.LearnActEndDateSpecified = true;
                    ld.LearnActEndDate = DateTime.Now.AddMonths(-1);
                    ld.CompStatusSpecified = true;
                    ld.CompStatus = (int)CompStatus.Completed;
                    ld.OutcomeSpecified = true;
                    ld.Outcome = (int)Outcome.Achieved;
                }
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDateSpecified = true;
                    ld.LearnStartDate = new DateTime(2017, 08, 01);
                    ld.LearnActEndDateSpecified = true;
                    ld.LearnActEndDate = DateTime.Now.AddMonths(-1);
                    ld.CompStatusSpecified = true;
                    ld.CompStatus = (int)CompStatus.Completed;
                }

                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
                var led = learner.LearningDelivery[0];
                var ldfams = led.LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateFrom = DateTime.Now.AddMonths(-4),
                    LearnDelFAMDateToSpecified = true,
                    LearnDelFAMDateTo = DateTime.Now.AddMonths(-2)
                });

                led.LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateTrainee(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60325999";
                learner.LearningDelivery[0].ProgType = (int)ProgType.Traineeship;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
            }
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
        }
    }
}
