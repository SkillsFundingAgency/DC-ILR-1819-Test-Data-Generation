using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R66
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
            return "R66";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R66";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions, ExclusionRecord = true }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                ld.AimTypeSpecified = true;
                ld.AimType = 3;
                ld.ProgTypeSpecified = true;
                ld.ProgType = 23;
                ld.FworkCodeSpecified = true;
                ld.FworkCode = 3;
                ld.PwayCodeSpecified = true;
                ld.PwayCode = 3;
                ld.StdCodeSpecified = true;
                ld.StdCode = 3;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }
    }
}
