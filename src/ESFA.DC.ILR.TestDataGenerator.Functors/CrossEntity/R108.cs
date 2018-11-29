using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R108
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
            return "R108";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R108";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = MutateESF, DoMutateOptions = MutateOptions },
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
