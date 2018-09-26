using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R70
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
            return "R70";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R70";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
             Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 3;
                ld.LearnAimRef = "60325999";
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 3;
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-Sep-01");
            options.LD.IncludeHHS = true;
            _options = options;
        }
    }
}
