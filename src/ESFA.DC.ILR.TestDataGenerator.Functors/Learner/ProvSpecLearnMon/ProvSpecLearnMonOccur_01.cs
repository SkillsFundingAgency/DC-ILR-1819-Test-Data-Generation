using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ProvSpecLearnMonOccur_01
        : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PSLMO_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateBA, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateBB, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateB, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                var ifamA = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur == ProvSpecLearnMonOccur.A.ToString()).ToList();
                var ifamB = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur == ProvSpecLearnMonOccur.B.ToString()).ToList();
                ifamA.ForEach(s => s.ProvSpecLearnMonOccur = ProvSpecLearnMonOccur.C.ToString());
                ifamB.ForEach(s => s.ProvSpecLearnMonOccur = "@");
                ifamA.AddRange(ifamB);
                learner.ProviderSpecLearnerMonitoring = ifamA.ToArray();
            }
        }

        private void MutateBA(MessageLearner learner, bool valid)
        {
            var ifam = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur != ProvSpecLearnMonOccur.A.ToString()).ToList();
            ifam.Add(new MessageLearnerProviderSpecLearnerMonitoring()
            {
                ProvSpecLearnMon = $"{learner.ULN}",
                ProvSpecLearnMonOccur = ProvSpecLearnMonOccur.A.ToString()
            });
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
            Mutate(learner, valid);
        }

        private void MutateBB(MessageLearner learner, bool valid)
        {
            var ifam = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur == ProvSpecLearnMonOccur.B.ToString()).ToList();
            ifam.Add(new MessageLearnerProviderSpecLearnerMonitoring()
            {
                ProvSpecLearnMon = $"{learner.ULN}",
                ProvSpecLearnMonOccur = ProvSpecLearnMonOccur.B.ToString()
            });
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
            Mutate(learner, valid);
        }

        private void MutateB(MessageLearner learner, bool valid)
        {
            var ifam = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur == ProvSpecLearnMonOccur.B.ToString()).ToList();
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
            Mutate(learner, valid);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.ProviderSpecialMonitoringRequired = true;
        }
    }
}
