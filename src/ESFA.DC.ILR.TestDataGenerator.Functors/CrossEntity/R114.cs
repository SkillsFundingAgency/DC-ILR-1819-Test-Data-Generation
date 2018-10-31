using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R114
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
            return "R114";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R114";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsLD2, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[1].FundModelSpecified = true;
                lds[1].FundModel = 35;
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
                lds[1].LearnAimRef = "50079335"; //10005158
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                lds[1].FundModelSpecified = true;
                lds[1].FundModel = 35;
                lds[1].AimTypeSpecified = true;
                lds[1].AimType = 3;
                lds[1].LearnAimRef = "10042702"; //10005158
            }
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            //options.CreateDestinationAndProgression = true;
            options.EmploymentRequired = true;
            _options = options;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
