using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R117
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
            return "R117";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R117";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearnerApp, DoMutateOptions = MutateGenerationOptionsLD2 },
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
                lds[0].AimType = 4;
                var ldfams = lds[1].LearningDeliveryFAM.ToList();
                    ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                    });

                    lds[1].LearningDeliveryFAM = ldfams.ToArray();
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
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
                var ldfams = lds[1].LearningDeliveryFAM.ToList();
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ProcuredAdultEducationBudget).ToString(),
                });

                lds[1].LearningDeliveryFAM = ldfams.ToArray();
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
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
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
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
