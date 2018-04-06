using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_25
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_25";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsOLASS, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
        }

        private void MutateLearningAim(MessageLearner learner)
        {
            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.Adult, LearnDelFAMCode.SOF_ESFA_Adult, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            Helpers.AddOrChangeSourceOfFunding(learner.LearningDelivery[0], LearnDelFAMCode.SOF_ESFA_1619);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeLDM = true;
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsOLASS(GenerationOptions options)
        {
            MutateGenerationOptions(options);
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_OLASS;
        }
    }
}
