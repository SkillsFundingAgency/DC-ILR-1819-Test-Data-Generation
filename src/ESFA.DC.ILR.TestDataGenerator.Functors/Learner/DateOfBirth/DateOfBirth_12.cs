using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_12
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DateOfBirth_12";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_12";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate19ASL2, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate19ASL3, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
        }

        private void Mutate19ASL2(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ASL.ToString()).First();
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.ASL_Neighbour).ToString();
        }

        private void Mutate19ASL3(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ASL.ToString()).First();
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.ASL_FamilyEnglishMathsLanguage).ToString();
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
