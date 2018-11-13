using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_08
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
            return "AFinType_08";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty08";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppfin, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateCommon(MessageLearner learner)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            learner.LearningDelivery[0].AppFinRecord = learner.LearningDelivery[0].AppFinRecord
                .Where(aft => aft.AFinType != LearnDelAppFinType.TNP.ToString()).ToArray();

            Helpers.AddAfninRecord(
                learner,
                LearnDelAppFinType.TNP.ToString(),
                (int)LearnDelAppFinCode.ResidualTrainingPrice,
                500);
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            MutateCommon(learner);

            if (!valid)
            {
                learner.LearningDelivery[0].FundModel = (int)FundModel.OtherAdult;
            }
        }

        private void MutateAppfin(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);

            if (!valid)
            {
                learner.LearningDelivery[0].AppFinRecord = learner.LearningDelivery[0].AppFinRecord
                    .Where(aft => aft.AFinType != LearnDelAppFinType.TNP.ToString()).ToArray();
                Helpers.AddAfninRecord(
                    learner,
                    LearnDelAppFinType.TNP.ToString(),
                    (int)LearnDelAppFinCode.TotalTrainingPrice,
                    500);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
