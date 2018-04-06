using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_55
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;
        private GenerationOptions _options;
        private List<string> _ALevelRefs;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_55";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            var result = new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }, // exclusion based on level
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLess24NVQLevel3, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }, // exclusion based on age
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsOLASS, ExclusionRecord = true }, // exclusion OLASS
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsRestart, ExclusionRecord = true }, // exclusion Restart (PriorLearnFundAdj_02)
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsMilitary }, // NOT exclusion Military DOB55 // LearnDelFAMType_06
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsSolent, ExclusionRecord = true }, // exclusion Solent // LearnDelFAMType_04
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24TradeUnionAim, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }, // exclusion Trade Unions
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsSpecialistCollege }, // specialist college
            };
            _ALevelRefs = new List<string>(10);
            _ALevelRefs.Add(_cache.LearnAimWithLearnAimType(LearnAimType.GCE_A_level).LearnAimRef);
            _ALevelRefs.Add(_cache.LearnAimWithLearnAimType(LearnAimType.GCE_A2_Level).LearnAimRef);
            _ALevelRefs.Add(_cache.LearnAimWithLearnAimType(LearnAimType.GCE_Applied_A_Level).LearnAimRef);
            _ALevelRefs.Add(_cache.LearnAimWithLearnAimType(LearnAimType.GCE_Applied_A_Level_Double_Award).LearnAimRef);
            _ALevelRefs.Add(_cache.LearnAimWithLearnAimType(LearnAimType.GCE_A_Level_with_GCE_Advanced_Subsidiary).LearnAimRef);
            foreach (var s in _ALevelRefs)
            {
                result.Add(
                    new LearnerTypeMutator() {
                        LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3ALevelLookup, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true, InvalidLines = 3, ValidLines = 3 });
            }

            result[10].InvalidLines = 1;
            result[10].ValidLines = 1;
            return result;
        }

        private void MutateLess24NVQLevel3(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less24, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            MutateNVQLevel3(learner, valid);
        }

        private void MutateNVQLevel3(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _cache.LearnAimFundingWithValidity(FundModel.Adult, LearnDelFAMCode.SOF_ESFA_Adult, _options.LD.OverrideLearnStartDate.Value).LearnAimRef;
        }

        private void Mutate24NVQLevel3(MessageLearner learner, bool valid)
        {
            MutateNVQLevel3(learner, valid);
            Mutate24(learner, valid);
        }

        private void Mutate24NVQLevel3ALevelLookup(MessageLearner learner, bool valid)
        {
            Mutate24NVQLevel3(learner, valid);
            learner.LearningDelivery[0].LearnAimRef = _ALevelRefs.First();
            _ALevelRefs.RemoveAt(0);
        }

        private void Mutate24(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less24, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.Older);
        }

        private void Mutate24TradeUnionAim(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _cache.LearnAimWithCategory(LearnDelCategory.TradeUnion).LearnAimRef;
            Mutate24(learner, valid);
        }

        private void MutateStartDate(GenerationOptions options)
        {
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-AUG-01");
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            MutateStartDate(options);
            options.LD.IncludeSOF = true;
            _options = options;
        }

        private void MutateGenerationOptionsMilitary(GenerationOptions options)
        {
            MutateStartDate(options);
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_Military;
            _options = options;
        }

        private void MutateGenerationOptionsSpecialistCollege(GenerationOptions options)
        {
            MutateStartDate(options);
            options.OverrideUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.SpecialistDesignatedCollege).UKPRN;
            _options = options;
        }

        private void MutateGenerationOptionsOLASS(GenerationOptions options)
        {
            MutateStartDate(options);
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_OLASS;
            _options = options;
        }

        private void MutateGenerationOptionsSolent(GenerationOptions options)
        {
            MutateStartDate(options);
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_SolentCity;
            _options = options;
        }

        private void MutateGenerationOptionsRestart(GenerationOptions options)
        {
            MutateStartDate(options);
            options.LD.IncludeRES = true;
            _options = options;
        }
    }
}
