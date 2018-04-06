using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class NINumber_01
        : ILearnerMultiMutator
    {
        private List<string> _invalidNi;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "NI_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _invalidNi = InvalidNINumber();
            var result = new List<LearnerTypeMutator>();
            for (int i = 0; i != _invalidNi.Count; ++i)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private List<string> InvalidNINumber()
        {
            List<char> invalid0 = new List<char> { 'D', 'F', 'I', 'Q', 'U', 'V' };
            List<char> invalid1 = new List<char> { 'D', 'F', 'I', 'O', 'Q', 'U', 'V' };
            List<string> invalid2 = new List<string> { "34567A", "3456S8", "345S78", "34F678", "3F5678", "T45678" };
            List<char> invalid3 = new List<char> { 'E', 'Z', '1' };
            List<string> result = new List<string>(50);

            foreach (var i0 in invalid0)
            {
                result.Add(i0 + "B345678A");
            }

            foreach (var i1 in invalid1)
            {
                result.Add("A" + i1 + "345678B");
            }

            foreach (var i2 in invalid2)
            {
                result.Add("TT" + i2 + "C");
            }

            foreach (var i3 in invalid3)
            {
                result.Add("TT345678" + i3);
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.NINumber = _invalidNi[0];
                _invalidNi.RemoveAt(0);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
