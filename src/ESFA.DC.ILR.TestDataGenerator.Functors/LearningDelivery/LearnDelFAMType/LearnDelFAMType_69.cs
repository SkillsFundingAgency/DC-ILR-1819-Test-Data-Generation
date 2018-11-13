using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_69
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
            return "LearnDelFAMType_69";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy69";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearnerCommunity, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }

            var ld = learner.LearningDelivery;
            foreach (MessageLearnerLearningDelivery lds in ld)
            {
                lds.SWSupAimId = Guid.NewGuid().ToString();
            }
        }

        private void MutateLearnerCommunity(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString(),
            });
            learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDateSpecified = true;
                learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 11, 20);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
