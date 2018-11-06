using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_84
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
            return "LearnAimRef_84";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR84";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateCategoryRef, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateNVQ2, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateDD07, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDM, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateSteelInd, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateCategoryRef, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2017, 07, 31).AddDays(1);
                var ldfams = ld.LearningDeliveryFAM.ToList();
                learner.DateOfBirth = ld.LearnStartDate.AddYears(-23);
                ld.LearnAimRef = "10034055";
            }

            foreach (var les in learner.LearnerEmploymentStatus)
            {
                les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
                les.DateEmpStatAppSpecified = true;
                les.EmpStatSpecified = true;
                les.EmpStat = 98;
            }
        }

        private void MutateCategoryRef(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "60186367";
                }
            }
        }

        private void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnStartDate = new DateTime(2017, 07, 31).AddDays(-1);
                }
            }
        }

        private void MutateNVQ2(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "50079190";
                }
            }
        }

        private void MutateDD07(MessageLearner learner, bool valid)
        {
            MutateCategoryRef(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgTypeSpecified = true;
                    ld.ProgType = (int)ProgType.IntermediateLevelApprenticeship;
                }
            }
        }

        private void MutateLDM(MessageLearner learner, bool valid)
        {
            MutateCategoryRef(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfams = ld.LearningDeliveryFAM.ToList();
                    ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                        LearnDelFAMCode = "034"
                    });
                    ld.LearningDeliveryFAM = ldfams.ToArray();
                }
            }
        }

        private void MutateRestarts(MessageLearner learner, bool valid)
        {
            MutateCategoryRef(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfams = ld.LearningDeliveryFAM.ToList();
                    ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                        LearnDelFAMCode = LearnDelFAMCode.RES.ToString()
                    });
                    ld.LearningDeliveryFAM = ldfams.ToArray();
                }
            }
        }

        private void MutateSteelInd(MessageLearner learner, bool valid)
        {
            MutateCategoryRef(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    var ldfams = ld.LearningDeliveryFAM.ToList();
                    ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                        LearnDelFAMCode = LearnDelFAMCode.LDM_SteelRedundancy.ToString()
                    });
                    ld.LearningDeliveryFAM = ldfams.ToArray();
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
