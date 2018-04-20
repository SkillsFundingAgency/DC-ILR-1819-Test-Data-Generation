using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnFAMType_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        private List<FAMTypeToCodes> _optionsOptionsPhase;
        private List<FAMTypeToCodes> _optionsMutatePhase;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _optionsOptionsPhase = new List<FAMTypeToCodes>()
            {
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { HighNeedsStudentRequired = true } }, FAM = LearnerFAMType.HNS, Valid = LearnerFAMCode.HNS_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EducationHealthCarePlanRequired = true } }, FAM = LearnerFAMType.EHC, Valid = LearnerFAMCode.EHC_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { MathsConditionOfFundingRequired = true } }, FAM = LearnerFAMType.MCF, Valid = LearnerFAMCode.MCF_ExcemptLearningDifficulty, Invalid = LearnerFAMCode.MCF_Unassigned1 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { MathsConditionOfFundingRequired = true } }, FAM = LearnerFAMType.MCF, Valid = LearnerFAMCode.MCF_ExcemptOverseasEquivalent, Invalid = LearnerFAMCode.MCF_Unassigned2 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { MathsConditionOfFundingRequired = true } }, FAM = LearnerFAMType.MCF, Valid = LearnerFAMCode.MCF_MetOtherInstitution, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { MathsConditionOfFundingRequired = true } }, FAM = LearnerFAMType.MCF, Valid = LearnerFAMCode.MCF_MetUKEquivalent, Invalid = (LearnerFAMCode)9 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EnglishConditionOfFundingRequired = true } }, FAM = LearnerFAMType.ECF, Valid = LearnerFAMCode.ECF_ExcemptLearningDifficulty, Invalid = LearnerFAMCode.ECF_Unassigned1 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EnglishConditionOfFundingRequired = true } }, FAM = LearnerFAMType.ECF, Valid = LearnerFAMCode.ECF_ExcemptOverseasEquivalent, Invalid = LearnerFAMCode.ECF_Unassigned2 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EnglishConditionOfFundingRequired = true } }, FAM = LearnerFAMType.ECF, Valid = LearnerFAMCode.ECF_MetOtherInstitution, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EnglishConditionOfFundingRequired = true } }, FAM = LearnerFAMType.ECF, Valid = LearnerFAMCode.ECF_MetUKEquivalent, Invalid = (LearnerFAMCode)9 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { SpecialEducationalNeedsRequired = true } }, FAM = LearnerFAMType.SEN, Valid = LearnerFAMCode.SEN_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { DisabledLearnerAllowanceRequired = true } }, FAM = LearnerFAMType.DLA, Valid = LearnerFAMCode.DLA_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_CareToLearn, Invalid = LearnerFAMCode.LSR_Unassigned1 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_Childcare, Invalid = LearnerFAMCode.LSR_Unassigned2 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_Discretionary, Invalid = LearnerFAMCode.LSR_Unassigned3 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_ESFWithChildcare, Invalid = LearnerFAMCode.LSR_Unassigned4 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_Hardship, Invalid = (LearnerFAMCode)9 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_Residential, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_ResidentialAccess, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { LearnerSupportRequired = true } }, FAM = LearnerFAMType.LSR, Valid = LearnerFAMCode.LSR_Vulnerable, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { NationalLearnerMonitoringRequired = true } }, FAM = LearnerFAMType.NLM, Valid = LearnerFAMCode.NLM_Merger, Invalid = LearnerFAMCode.NLM_Unassigned1 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { NationalLearnerMonitoringRequired = true } }, FAM = LearnerFAMType.NLM, Valid = LearnerFAMCode.NLM_ContractLevel, Invalid = LearnerFAMCode.NLM_Unassigned2 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { NationalLearnerMonitoringRequired = true } }, FAM = LearnerFAMType.NLM, Valid = LearnerFAMCode.NLM_Merger, Invalid = LearnerFAMCode.NLM_Unassigned3 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { NationalLearnerMonitoringRequired = true } }, FAM = LearnerFAMType.NLM, Valid = LearnerFAMCode.NLM_Merger, Invalid = LearnerFAMCode.NLM_Unassigned4 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EFADisadvantageFundingRequired = true } }, FAM = LearnerFAMType.EDF, Valid = LearnerFAMCode.EDF_EnglishNotGot, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EFADisadvantageFundingRequired = true } }, FAM = LearnerFAMType.EDF, Valid = LearnerFAMCode.EDF_MathsNotGot, Invalid = (LearnerFAMCode)99 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { FreeMealEntitlementRequired = true } }, FAM = LearnerFAMType.FME, Valid = LearnerFAMCode.FME_1415Eligible, Invalid = (LearnerFAMCode)99 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { FreeMealEntitlementRequired = true } }, FAM = LearnerFAMType.FME, Valid = LearnerFAMCode.FME_1619Receipt, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { PupilPremiumEligibilityRequired = true } }, FAM = LearnerFAMType.PPE, Valid = LearnerFAMCode.PPE_AdoptedCare, Invalid = LearnerFAMCode.PPE_Unassigned1 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { PupilPremiumEligibilityRequired = true } }, FAM = LearnerFAMType.PPE, Valid = LearnerFAMCode.PPE_ServiceChild, Invalid = LearnerFAMCode.PPE_Unassigned2 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { PupilPremiumEligibilityRequired = true } }, FAM = LearnerFAMType.PPE, Valid = LearnerFAMCode.PPE_AdoptedCare, Invalid = LearnerFAMCode.PPE_Unassigned3 },
            };
            _optionsMutatePhase = new List<FAMTypeToCodes>(10);
            var result = new List<LearnerTypeMutator>();
            _optionsOptionsPhase.ForEach(s => result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }));
            return result;
        }

        public string RuleName()
        {
            return "LearnFAMType_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LFam_01";
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            LearnerFAMType fam = _optionsMutatePhase[0].FAM;
            Helpers.AddOrChangeLearnerFAM(learner, fam, _optionsMutatePhase[0].Valid);
            if (fam == LearnerFAMType.ECF)
            {
                Helpers.AddOrChangeLearnerFAM(learner, LearnerFAMType.EDF, LearnerFAMCode.EDF_EnglishNotGot);
            }

            if (!valid)
            {
                Helpers.AddOrChangeLearnerFAM(learner, fam, _optionsMutatePhase[0].Invalid);
            }

            _optionsMutatePhase.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.FAM = _optionsOptionsPhase[0].Options.FAM;
            if (!options.FAM.SpecialEducationalNeedsRequired)
            {
                options.FAM.EducationHealthCarePlanRequired = true;
            }

            _optionsOptionsPhase[0].Options = options;
            _optionsMutatePhase.Add(_optionsOptionsPhase[0]);

            _optionsOptionsPhase.RemoveAt(0);
        }
    }
}
