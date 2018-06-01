using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_27
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DateOfBirth_27";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_27";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ValidLines = 3, InvalidLines = 4 }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact1Day, Helpers.BasedOn.AYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.IncludeLDM = true;
            options.LD.IncludeSOF = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_OLASS;
        }
    }
}
