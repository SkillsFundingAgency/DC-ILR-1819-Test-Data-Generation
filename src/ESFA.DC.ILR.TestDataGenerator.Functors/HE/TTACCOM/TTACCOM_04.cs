using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class TTACCOM_04 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "TTACCOM_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "TTACCOM04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        public void Mutate(MessageLearner learner, bool valid)
        {
            var hes = new List<MessageLearnerLearningDeliveryLearningDeliveryHE>(4);

            var lhe = new List<MessageLearnerLearnerHE>(2);
            lhe.Add(new MessageLearnerLearnerHE()
            {
                UCASPERID = "1234567899"
            });

            var Options = new GenerationOptions()
            {
                LD = new LearningDeliveryOptions()
                {
                    IncludeHEFields = true
                }
            };
            var what = Options.LD.IncludeHEFields;
            if (valid)
            {
                hes.Add(new MessageLearnerLearningDeliveryLearningDeliveryHE()
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
                    NETFEE = 0,
                    NETFEESpecified = true,
                    GROSSFEE = 1,
                    GROSSFEESpecified = true,
                    DOMICILE = "ZZ",
                    ELQ = (int)EquivalentLowerQualification.NotRequired,
                    ELQSpecified = true
                });
            }

            if (!valid)
            {
                hes.Add(new MessageLearnerLearningDeliveryLearningDeliveryHE()
                {
                    NUMHUS = "2000812012XTT60021",
                    QUALENT3 = QualificationOnEntry.X06.ToString(),
                    UCASAPPID = "AB89",
                    TYPEYR = (int)TypeOfyear.FEYear,
                    TYPEYRSpecified = true,
                    MODESTUD = (int)ModeOfStudy.FullTime,
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
                    NETFEE = 0,
                    NETFEESpecified = true,
                    GROSSFEE = 1,
                    GROSSFEESpecified = true,
                    DOMICILE = "ZZ",
                    ELQ = (int)EquivalentLowerQualification.NotRequired,
                    ELQSpecified = true
                });
                learner.LearnerHE = lhe.ToArray();
            }

            foreach (var lrnr in learner.LearningDelivery)
            {
                lrnr.LearningDeliveryHE = hes.ToArray();
            }
        }

        public void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "50111152";
                learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2010-OCT-01").AddDays(-1);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
