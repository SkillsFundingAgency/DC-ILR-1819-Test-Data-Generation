using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R64
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
            return "R64";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R64";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateProgType, DoMutateOptions = MutateOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnAimRef = "50104767";
                lds[0].AimTypeSpecified = true;
                lds[0].AimType = 3;
                lds[0].FworkCodeSpecified = true;
                lds[0].FworkCode = 420;
                lds[0].PwayCodeSpecified = true;
                lds[0].PwayCode = 1;
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(+10);
                lds[0].CompStatusSpecified = true;
                lds[0].CompStatus = 2;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = 1;

                lds[1].LearnAimRef = "50112089";
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
                lds[1].FworkCodeSpecified = true;
                lds[1].FworkCode = 420;
                lds[1].PwayCodeSpecified = true;
                lds[1].PwayCode = 1;
                lds[1].LearnStartDateSpecified = true;
                lds[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(+11);
                lds[1].CompStatusSpecified = true;
                lds[1].CompStatus = 1;
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnAimRef = "50104767";
                lds[0].AimTypeSpecified = true;
                lds[0].AimType = 3;
                lds[0].ProgTypeSpecified = true;
                lds[0].ProgType = (int)ProgType.ApprenticeshipStandard;
                lds[0].FworkCodeSpecified = true;
                lds[0].FworkCode = 420;
                lds[0].PwayCodeSpecified = true;
                lds[0].PwayCode = 1;
                lds[0].LearnActEndDateSpecified = true;
                lds[0].LearnActEndDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(+10);
                lds[0].CompStatusSpecified = true;
                lds[0].CompStatus = 2;
                lds[0].OutcomeSpecified = true;
                lds[0].Outcome = 1;

                lds[1].LearnAimRef = "50112089";
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
                lds[1].FworkCodeSpecified = true;
                lds[1].FworkCode = 420;
                lds[1].PwayCodeSpecified = true;
                lds[1].PwayCode = 1;
                lds[1].LearnStartDateSpecified = true;
                lds[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate.AddMonths(+11);
                lds[1].CompStatusSpecified = true;
                lds[1].CompStatus = 1;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.GenerateMultipleLDs = 2;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }
    }
}
