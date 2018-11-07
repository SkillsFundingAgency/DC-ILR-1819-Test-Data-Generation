using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_86
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
            return "LearnAimRef_86";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR86";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnAimRef, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnTraineeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateSteelRedundancy, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearnAimRef, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2017, 07, 31).AddDays(1);
                ld.LearnAimRef = "60110016";
            }
        }

        private void MutateLearnAimRef(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "ZWRKX001";
                }
            }
        }

        private void MutateLearnTraineeship(MessageLearner learner, bool valid)
        {
            MutateLearnAimRef(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgTypeSpecified = true;
                    ld.ProgType = (int)ProgType.Traineeship;
                }
            }
        }

        private void MutateSteelRedundancy(MessageLearner learner, bool valid)
        {
            MutateLearnAimRef(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
