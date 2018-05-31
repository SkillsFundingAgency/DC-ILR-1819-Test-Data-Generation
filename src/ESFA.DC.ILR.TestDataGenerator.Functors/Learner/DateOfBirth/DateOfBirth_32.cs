using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_32
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DateOfBirth_32";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_32";
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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate24NVQLevel3Restart, DoMutateOptions = MutateGenerationOptionsSpecialistCollege, ExclusionRecord = true } // specialist college
            };
        }

        private void MutateLess24NVQLevel3(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less24, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            MutateNVQLevel3(learner, valid);
        }

        private void MutateNVQLevel3(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "50104767";
        }

        private void Mutate24NVQLevel3(MessageLearner learner, bool valid)
        {
            MutateNVQLevel3(learner, valid);
            Mutate24(learner, valid);
        }

        private void Mutate24NVQLevel3Restart(MessageLearner learner, bool valid)
        {
            Mutate24NVQLevel3(learner, valid);
            Helpers.AddLearningDeliveryRestartFAM(learner);
            learner.LearningDelivery[0].PriorLearnFundAdj = 10;
            learner.LearningDelivery[0].PriorLearnFundAdjSpecified = true;
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
            options.LD.OverrideLearnStartDate = DateTime.Parse("2016-JUL-30");
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsMilitary(GenerationOptions options)
        {
            options.LD.IncludeLDM = true;
            options.LD.OverrideLDM = (int)LearnDelFAMCode.LDM_Military;
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
        }

        private void MutateGenerationOptionsRestart(GenerationOptions options)
        {
            options.LD.IncludeRES = true;
        }
    }
}
