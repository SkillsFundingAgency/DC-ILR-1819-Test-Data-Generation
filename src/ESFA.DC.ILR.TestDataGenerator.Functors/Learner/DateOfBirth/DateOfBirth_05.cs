using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_05
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_05";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = Mutate4AndADay, DoMutateOptions = MutateGenerationOptionsCL },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate4AndADay, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void Mutate4AndADay(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact4, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
            try
            {
                var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ASL.ToString()).First();
                fam.LearnDelFAMCode = ((int)LearnDelFAMCode.ASL_WiderFamily).ToString();
            }
            catch { } // swallow is fine - means no ASL block added which is ok
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsCL(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}
