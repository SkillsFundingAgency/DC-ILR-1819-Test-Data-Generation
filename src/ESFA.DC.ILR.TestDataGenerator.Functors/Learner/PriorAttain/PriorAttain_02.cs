using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PriorAttain_02
        : ILearnerMultiMutator
    {
        private List<PriorAttainWithLearnAimRef> _attain;
        private ILearnerCreatorDataCache _cache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PAtt_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            _attain = new List<PriorAttainWithLearnAimRef>()
            {
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.NotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level2, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.OtherNotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level3, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.NotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level3, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.OtherNotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level2, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.NotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level2, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.OtherNotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level3, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.NotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level3, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.OtherNotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level2, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.NotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level3, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.OtherNotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level2, FundModel.Adult).LearnAimRef },
                new PriorAttainWithLearnAimRef() { Attain = PriorAttain.OtherNotKnown, LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level2, FundModel.Adult).LearnAimRef },
            };

            var result = new List<LearnerTypeMutator>();
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ValidLines = 1 });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ValidLines = 1 });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ValidLines = 1 });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ValidLines = 1 });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsLDM, ExclusionRecord = true });
            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _attain[0].LearnAimRef;
            if (!valid)
            {
                learner.PriorAttain = (int)_attain[0].Attain;
            }

            _attain.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeLDM = false;
        }

        private void MutateGenerationOptionsLDM(GenerationOptions options)
        {
        }
    }
}
