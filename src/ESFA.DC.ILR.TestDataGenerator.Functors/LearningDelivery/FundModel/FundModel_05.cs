using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_05
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
            };
        }

        public string RuleName()
        {
            return "FundModel_05";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FM_05";
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
                {
                    ld.LearnStartDate = ld.LearnStartDate.AddDays(-1);
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHHS = true;
            _options = options;
            _options.LD.OverrideLearnStartDate = DateTime.Parse("2017-MAY-01");
        }
    }
}
