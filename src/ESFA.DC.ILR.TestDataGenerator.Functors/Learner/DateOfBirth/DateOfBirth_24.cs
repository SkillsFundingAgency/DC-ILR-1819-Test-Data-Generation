using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_24
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_24";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            MutateLearningAim(learner);
            if (!valid)
            {
                learner.DateOfBirthSpecified = false;
            }
        }

        private void MutateLearningAim(MessageLearner learner)
        {
            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeADL = true;
            options.LD.IncludeLDM = true;
            options.LD.IncludeHEFields = true;
            options.LD.IncludeSOF = true;
        }
    }
}
