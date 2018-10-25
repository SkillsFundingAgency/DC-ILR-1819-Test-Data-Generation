using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R107
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
            return "R107";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R107";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateESF, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateInvalidLD2, DoMutateOptions = MutateOptionLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateInvalidLD2, DoMutateOptions = MutateOptionLD2Prog, DoMutateProgression = MutateProgression },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateValidLD2, DoMutateOptions = MutateOptionLD2, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateCommunity, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression, ExclusionRecord = true },
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

        private void MutateESF(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            MutateValid(learner, valid);

            if (!valid)
            {
                MutateInvalid(learner, valid);
            }
        }

        private void MutateCommunity(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            foreach (var lds in learner.LearningDelivery)
            {
                lds.CompStatusSpecified = true;
                lds.CompStatus = (int)CompStatus.Continuing;
            }

            var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
            });
            learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();

            if (!valid)
            {
                MutateInvalid(learner, valid);
            }
        }

        private void MutateInvalid(MessageLearner learner, bool valid)
        {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnActEndDateSpecified = true;
                    ld.LearnActEndDate = ld.LearnStartDate.AddMonths(6);
                    ld.CompStatusSpecified = true;
                    ld.CompStatus = (int)CompStatus.Withdrawn;
                    ld.OutcomeSpecified = true;
                    ld.Outcome = (int)Outcome.NoAchievement;
                }
        }

        private void MutateInvalidLD2(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            MutateValid(learner, valid);

            if (!valid)
            {
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = lds[0].LearnStartDate.AddMonths(6);
                lds[0].CompStatusSpecified = true;
                lds[0].CompStatus = (int)CompStatus.Withdrawn;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.NoAchievement;
                lds[1].LearnActEndDateSpecified = true;
                lds[1].LearnActEndDate = lds[1].LearnStartDate.AddMonths(10);
                lds[1].CompStatusSpecified = true;
                lds[1].CompStatus = (int)CompStatus.Withdrawn;
                lds[1].OutcomeSpecified = true;
                lds[1].Outcome = (int)Outcome.NoAchievement;
            }
        }

        private void MutateValidLD2(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            MutateValid(learner, valid);

            if (!valid)
            {
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = lds[0].LearnStartDate.AddMonths(6);
                lds[0].CompStatusSpecified = true;
                lds[0].CompStatus = (int)CompStatus.Withdrawn;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = (int)Outcome.NoAchievement;
                lds[1].LearnActEndDateSpecified = true;
                lds[1].LearnActEndDate = lds[1].LearnStartDate.AddMonths(10);
                lds[1].CompStatusSpecified = true;
                lds[1].CompStatus = (int)CompStatus.BreakInLearning;
                lds[1].OutcomeSpecified = true;
                lds[1].Outcome = (int)Outcome.NoAchievement;
            }
        }

        private void MutateValid(MessageLearner learner, bool valid)
        {
            foreach (var lds in learner.LearningDelivery)
            {
                lds.LearnActEndDateSpecified = true;
                lds.LearnActEndDate = lds.LearnStartDate.AddMonths(8);
                lds.CompStatusSpecified = true;
                lds.CompStatus = (int)CompStatus.BreakInLearning;
                lds.OutcomeSpecified = true;
                lds.Outcome = (int)Outcome.NoAchievement;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }

        private void MutateOptionLD2(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
        }

        private void MutateOptionLD2Prog(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            options.CreateDestinationAndProgression = true;
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

        private void MutateProgressionDate(MessageLearnerDestinationandProgression learner, bool valid)
        {
            if (!valid)
            {
                var dpout = learner.DPOutcome.ToList();
                dpout.Add(new MessageLearnerDestinationandProgressionDPOutcome()
                {
                    OutCode = 1,
                    OutCodeSpecified = true,
                    OutType = "VOL",
                    OutStartDateSpecified = true,
                    OutStartDate = new DateTime(2017, 11, 28),
                    OutCollDate = new DateTime(2017, 11, 30),
                    OutCollDateSpecified = true
                });
                learner.DPOutcome = dpout.Skip(1).ToArray();
            }
        }
    }
}
