using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R118
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "R118";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R118";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnerApp, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsLD2, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].AimTypeSpecified = true;
                lds[0].AimType = 1;
                lds[0].ProgTypeSpecified = true;
                lds[0].ProgType = (int)ProgType.Traineeship;
                var ldfams = lds[0].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });

                lds[0].LearningDeliveryFAM = ldfams.ToArray();
                var ldfams1 = lds[1].LearningDeliveryFAM.ToList();
                    ldfams1.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                    });

                    lds[1].LearningDeliveryFAM = ldfams1.ToArray();
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 4;
                lds[1].ProgTypeSpecified = true;
                lds[1].ProgType = (int)ProgType.Traineeship;
            }
        }

        private void MutateLearnerApp(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].ProgTypeSpecified = true;
                lds[0].ProgType = (int)ProgType.Traineeship;
                var ldfams = lds[0].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });

                lds[0].LearningDeliveryFAM = ldfams.ToArray();
                var ldfams1 = lds[1].LearningDeliveryFAM.ToList();
                ldfams1.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });

                lds[1].LearningDeliveryFAM = ldfams1.ToArray();
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 4;
                lds[1].ProgTypeSpecified = true;
                lds[1].ProgType = (int)ProgType.Traineeship;
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[0].AimTypeSpecified = true;
                lds[0].AimType = 1;
                lds[0].ProgTypeSpecified = true;
                lds[0].ProgType = (int)ProgType.Traineeship;
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
                var ldfams = lds[0].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });

                lds[0].LearningDeliveryFAM = ldfams.ToArray();
                var ldfams1 = lds[1].LearningDeliveryFAM.ToList();
                ldfams1.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });

                lds[1].LearningDeliveryFAM = ldfams1.ToArray();
                lds[1].ProgTypeSpecified = true;
                lds[1].ProgType = (int)ProgType.Traineeship;
            }
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.GenerateMultipleLDs = 2;
            _options = options;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
