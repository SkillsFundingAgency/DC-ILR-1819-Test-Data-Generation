using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class HEPostCode_01
        : ILearnerMultiMutator
    {
        private List<string> _nonExistPostcodes;
        private List<string> _validPostcodes;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "HEPostCode_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "HEPoCd01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _validPostcodes = cache.ValidPostcode().ToList();
            _nonExistPostcodes = cache.NonExistPostcode().ToList();
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutatePostOne, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutatePostTwo, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutatePostThree, DoMutateOptions = MutateGenerationOptions }
            };
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

        private void MutatePostOne(MessageLearner learner, bool valid)
        {
            if (valid)
            {
               MutateHE(learner, _validPostcodes[0]);
            }

            if (!valid)
            {
                MutateHE(learner, _nonExistPostcodes[0]);
            }
        }

        private void MutatePostTwo(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateHE(learner, _validPostcodes[1]);
            }

            if (!valid)
            {
                MutateHE(learner, _nonExistPostcodes[1]);
            }
        }

        private void MutatePostThree(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                MutateHE(learner, _validPostcodes[2]);
            }

            if (!valid)
            {
                MutateHE(learner, _nonExistPostcodes[2]);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
