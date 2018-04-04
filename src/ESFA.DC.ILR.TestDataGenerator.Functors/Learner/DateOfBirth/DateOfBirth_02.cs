using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_02
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHEFCE, DoMutateOptions = MutateGenerationOptionsHEFCE, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateCL, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHEFCE, DoMutateOptions = MutateGenerationOptionsADL, ExclusionRecord = true, InvalidLines = 2 }
            };
        }

        private void MutateHEFCE(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            learner.LearningDelivery[0].LearningDeliveryFAM[0].LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString();
            if (!valid)
            {
                learner.DateOfBirthSpecified = false;
            }
        }

        private void MutateCL(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirthSpecified = false;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeSOF = true;
            options.LD.IncludeLDM = true;
        }

        private void MutateGenerationOptionsHEFCE(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeSOF = true;
            options.LD.IncludeHEFields = true;
        }

        private void MutateGenerationOptionsADL(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeADL = true; // need this for the exception record
            options.LD.IncludeHEFields = true;
            options.LD.IncludeSOF = true;
        }
    }
}
