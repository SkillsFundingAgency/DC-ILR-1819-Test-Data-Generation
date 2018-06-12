using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class Accom_01 : ILearnerMultiMutator
    {
        private List<Accomodation> _accomodation;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "Accom_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Accm_01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _accomodation = new List<Accomodation>(30);
            var result = new List<LearnerTypeMutator>();
            foreach (var eth in Enum.GetValues(typeof(Accomodation)))
            {
                _accomodation.Add((Accomodation)eth);
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.Accom = (int)_accomodation[0];
            _accomodation.RemoveAt(0);
            if (!valid)
            {
                learner.Accom += 1;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.AccomodationRequired = true;
        }
    }
}
