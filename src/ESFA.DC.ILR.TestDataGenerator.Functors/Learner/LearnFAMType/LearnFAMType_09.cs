using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnFAMType_09
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        private List<FAMTypeToCodes> _optionsOptionsPhase;
        private List<FAMTypeToCodes> _optionsMutatePhase;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LearnFAMType_09";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LFam_09";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _optionsOptionsPhase = new List<FAMTypeToCodes>()
            {
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { HighNeedsStudentRequired = true } }, FAM = LearnerFAMType.HNS, Valid = LearnerFAMCode.HNS_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EducationHealthCarePlanRequired = true } }, FAM = LearnerFAMType.EHC, Valid = LearnerFAMCode.EHC_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { MathsConditionOfFundingRequired = true } }, FAM = LearnerFAMType.MCF, Valid = LearnerFAMCode.MCF_ExcemptLearningDifficulty, Invalid = LearnerFAMCode.MCF_MetOtherInstitution },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { EnglishConditionOfFundingRequired = true } }, FAM = LearnerFAMType.ECF, Valid = LearnerFAMCode.ECF_ExcemptLearningDifficulty, Invalid = LearnerFAMCode.ECF_MetOtherInstitution },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { SpecialEducationalNeedsRequired = true } }, FAM = LearnerFAMType.SEN, Valid = LearnerFAMCode.SEN_Yes, Invalid = 0 },
                new FAMTypeToCodes() { Options = new GenerationOptions() { FAM = new LearnerFAMOptions() { DisabledLearnerAllowanceRequired = true } }, FAM = LearnerFAMType.DLA, Valid = LearnerFAMCode.DLA_Yes, Invalid = 0 },
            };
            _optionsMutatePhase = new List<FAMTypeToCodes>(10);
            var result = new List<LearnerTypeMutator>();
            _optionsOptionsPhase.ForEach(s => result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions }));
            return result;
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
                Helpers.AddLearnerFAM(learner, fam, _optionsMutatePhase[0].Invalid);
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
