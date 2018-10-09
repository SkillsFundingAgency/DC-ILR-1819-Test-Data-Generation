using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class HEPostCode_02
        : ILearnerMultiMutator
    {
        private List<string> _invalidPostcode;
        private List<string> _validPostcode;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "HEPostCode_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "HEPoCd01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _invalidPostcode = cache.InvalidPostcode().ToList();
            _validPostcode = cache.ValidPostcode().ToList();
            var result = new List<LearnerTypeMutator>();
            for (int i = 0; i != _invalidPostcode.Count; ++i)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        public void MutateHE(MessageLearner learner, string postcode)
        {
            var ldhe = new List<MessageLearnerLearningDeliveryLearningDeliveryHE>();

            ldhe.Add(new MessageLearnerLearningDeliveryLearningDeliveryHE()
            {
                NUMHUS = "2000812012XTT60021",
                QUALENT3 = QualificationOnEntry.X06.ToString(),
                UCASAPPID = "AB89",
                TYPEYR = (int)TypeOfyear.FEYear,
                TYPEYRSpecified = true,
                MODESTUD = (int)ModeOfStudy.NotInPopulation,
                MODESTUDSpecified = true,
                FUNDLEV = (int)FundingLevel.Undergraduate,
                FUNDLEVSpecified = true,
                FUNDCOMP = (int)FundingCompletion.NotYetCompleted,
                FUNDCOMPSpecified = true,
                STULOAD = 10.0M,
                STULOADSpecified = true,
                YEARSTU = 1,
                YEARSTUSpecified = true,
                MSTUFEE = (int)MajorSourceOfTuitionFees.NoAward,
                MSTUFEESpecified = true,
                PCFLDCS = 100,
                PCFLDCSSpecified = true,
                SPECFEE = (int)SpecialFeeIndicator.Other,
                SPECFEESpecified = true,
                NETFEE = 9000,
                NETFEESpecified = true,
                GROSSFEE = 30000,
                GROSSFEESpecified = true,
                DOMICILE = "ZZ",
                ELQ = (int)EquivalentLowerQualification.NotRequired,
                ELQSpecified = true,
                HEPostCode = postcode
            });

            learner.LearningDelivery[0].LearningDeliveryHE = ldhe.ToArray();
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateHE(learner, _validPostcode[0]);
            }

            if (!valid)
            {
                MutateHE(learner, _invalidPostcode[0]);
                _invalidPostcode.RemoveAt(0);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
