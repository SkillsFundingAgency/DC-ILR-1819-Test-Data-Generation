using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_78
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
            return "LearnAimRef_78";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR78";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLevel3Percent, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLevel3PercentNull, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAppreticeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateOlass, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateCategoryRef, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutatePriorAttain1, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAimEffectiveFrom, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2016, 08, 01).AddDays(-1);
                var ldfams = ld.LearningDeliveryFAM.ToList();
                learner.DateOfBirth = ld.LearnStartDate.AddYears(-25);
                ld.LearnAimRef = "60110016";

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_Military).ToString()
                });
                ld.LearningDeliveryFAM = ldfams.ToArray();
            }

            foreach (var les in learner.LearnerEmploymentStatus)
                {
                    les.DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(-1);
                    les.DateEmpStatAppSpecified = true;
                    les.EmpStatSpecified = true;
                    les.EmpStat = 98;
            }
        }

        private void MutatePriorAttain(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = (int)PriorAttain.Level4;
            }
        }

        private void MutateAppreticeship(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
            }
        }

        private void MutateOlass(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = "034"
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateRestarts(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateCategoryRef(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60077207";
            }
        }

        private void MutatePriorAttain1(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.PriorAttainSpecified = false;
            }
        }

        private void MutateLevel3Percent(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "60156466";
                }
            }
        }

        private void MutateLevel3PercentNull(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "6014998X";
                }
            }
        }

        private void MutateAimEffectiveFrom(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.LearnAimRef = "60330211";
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
