using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Sex_01
        : ILearnerMultiMutator
    {
        private List<Sex> _sex;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "Sex_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Sex_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _sex = new List<Sex>(30);
            var result = new List<LearnerTypeMutator>();
            foreach (var eth in Enum.GetValues(typeof(Sex)))
            {
                _sex.Add((Sex)eth);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.Sex = _sex[0].ToString();
            _sex.RemoveAt(0);
            if (!valid)
            {
                learner.Sex = "I";
                //if (_sex.Count == 0)
                //{
                //    learner.Sex = string.Empty;
                //}
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
