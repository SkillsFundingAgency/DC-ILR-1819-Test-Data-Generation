using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_13
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_13";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate16, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate16(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact16, Helpers.BasedOn.EndAYYear, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);

            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.LD.IncludeSOF = true;
        }
    }
}
