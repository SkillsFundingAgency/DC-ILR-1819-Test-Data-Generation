using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_49
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DateOfBirth_49";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_49";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }, // exclusion based on level
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLess24NVQLevel3, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }, // exclusion based on age
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsOLASS, ExclusionRecord = true }, // exclusion OLASS
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsRestart, ExclusionRecord = true }, // exclusion OLASS
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsMilitary, ExclusionRecord = true }, // exclusion Military
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsSolent, ExclusionRecord = true }, // exclusion Solent
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24TradeUnionAim, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }, // exclusion Trade Unions
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3, DoMutateOptions = MutateGenerationOptionsSpecialistCollege, ExclusionRecord = true } // specialist college
            };
        }

        private void MutateLess24NVQLevel3(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less24, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            MutateNVQLevel3(learner, valid);
        }

        private void MutateNVQLevel3(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _cache.LearnAimWithLevel(FullLevel.Level3, FundModel.Adult).LearnAimRef;
        }

        private void Mutate24NVQLevel3(MessageLearner learner, bool valid)
        {
            MutateNVQLevel3(learner, valid);
            Mutate24(learner, valid);
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

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-JUL-30");
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsMilitary(GenerationOptions options)
        {
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_Military;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2015-OCT-30");
        }

        private void MutateGenerationOptionsSpecialistCollege(GenerationOptions options)
        {
            options.OverrideUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.SpecialistDesignatedCollege).UKPRN;
        }

        private void MutateGenerationOptionsOLASS(GenerationOptions options)
        {
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_OLASS;
        }

        private void MutateGenerationOptionsSolent(GenerationOptions options)
        {
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_SolentCity;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2015-OCT-30");
            options.AddProvSpecLearnMonA = "LDM 339 will error";
        }

        private void MutateGenerationOptionsRestart(GenerationOptions options)
        {
            options.LD.IncludeRES = true;
        }
    }
}
