using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PCFLDCS_02 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PCFLDCS_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PCFLDCS02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateNoHE, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        public void MutateHE(MessageLearner learner, bool valid)
        {
            var hes = new List<MessageLearnerLearningDeliveryLearningDeliveryHE>(4);
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
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
                learner.LearningDelivery[0].LearnAimRef = "40005240";
            }

            foreach (var lrnr in learner.LearningDelivery)
            {
                lrnr.LearningDeliveryHE = hes.ToArray();
            }
        }

        public void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            MutateHE(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnAimRef = "60316482";
            }
        }

        public void MutateNoHE(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = new DateTime(1998, 07, 01);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
