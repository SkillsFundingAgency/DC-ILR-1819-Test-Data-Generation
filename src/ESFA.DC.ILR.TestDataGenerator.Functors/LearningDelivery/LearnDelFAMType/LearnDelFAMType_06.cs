using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_06
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
            return "LearnDelFAMType_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateNSA, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 09, 01);
                var led1 = learner.LearningDelivery[0];
                var ldfams1 = led1.LearningDeliveryFAM.ToList();
                ldfams1.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.NSA.ToString(),
                    LearnDelFAMCode = "14",
                 });
                led1.LearningDeliveryFAM = ldfams1.ToArray();
            }
        }

        private void MutateNSA(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2016, 07, 31);
                var led1 = learner.LearningDelivery[0];
                var ldfams1 = led1.LearningDeliveryFAM.ToList();
                ldfams1.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.NSA.ToString(),
                    LearnDelFAMCode = "14",
                });
                led1.LearningDeliveryFAM = ldfams1.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
