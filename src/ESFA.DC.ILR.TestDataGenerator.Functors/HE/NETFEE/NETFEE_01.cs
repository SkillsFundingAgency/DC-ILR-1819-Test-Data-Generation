﻿using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class NETFEE_01 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "NETFEE_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "NETFEE01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHE, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateLearnStartDate, DoMutateOptions = MutateGenerationOptionsHE,  ExclusionRecord = true }
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
                    SSN = "WADM46891352A",
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
                    PCFLDCS = 100,
                    PCFLDCSSpecified = true,
                    SPECFEE = (int)SpecialFeeIndicator.Other,
                    SPECFEESpecified = true,
                    GROSSFEE = 1,
                    GROSSFEESpecified = true,
                    DOMICILE = "ZZ",
                    ELQ = (int)EquivalentLowerQualification.NotRequired,
                    ELQSpecified = true
                });
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.SOF);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
            }

            foreach (var lrnr in learner.LearningDelivery)
            {
                lrnr.LearningDeliveryHE = hes.ToArray();
            }
        }

        public void MutateLearnStartDate(MessageLearner learner, bool valid)
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
                    SSN = "WADM46891352A",
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
                    GROSSFEE = 1,
                    GROSSFEESpecified = true,
                    DOMICILE = "ZZ",
                    ELQ = (int)EquivalentLowerQualification.NotRequired,
                    ELQSpecified = true
                });
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.SOF);
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_HEFCE).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
            }

            foreach (var lrnr in learner.LearningDelivery)
            {
                lrnr.LearningDeliveryHE = hes.ToArray();
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.EmploymentRequired = true;
        }
    }
}
