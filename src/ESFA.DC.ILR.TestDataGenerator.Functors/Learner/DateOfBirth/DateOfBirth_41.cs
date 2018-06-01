using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_41
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private List<LearnDelFAMCode> _excludedLDMs = new List<LearnDelFAMCode>()
        {
            LearnDelFAMCode.LDM_OLASS,
            LearnDelFAMCode.LDM_NonApprenticeshipSportingExcellence,
            LearnDelFAMCode.LDM_NonApprenticeshipTheatre,
            LearnDelFAMCode.LDM_NonApprenticeshipSeaFishing,
            LearnDelFAMCode.LDM_SteelRedundancy // not exclusion
        };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            var result = new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Standard, DoMutateOptions = MutateGenerationOptionsStandards, ExclusionRecord = true, ValidLines = 1 },
            };
            foreach (var v in _excludedLDMs)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate19LDM, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = v != LearnDelFAMCode.LDM_SteelRedundancy });
            }

            return result;
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHHS = true;
        }

        public void MutateGenerationOptionsStandards(GenerationOptions options)
        {
            // has to be before MAY-17 because of FM36 rules
            options.LD.OverrideLearnStartDate = DateTime.Parse("2016-JUL-31");
 //           options.CreateDestinationAndProgression = true;
            options.LD.IncludeHHS = true;
            _options = options;
        }

        public string RuleName()
        {
            return "DateOfBirth_41";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_41";
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
        }

        private void Mutate19Standard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            Helpers.MutateApprenticeshipToStandard(learner, FundModel.OtherAdult);
            Mutate19(learner, valid);
            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void Mutate19LDM(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            Helpers.MutateLearningDeliveryMonitoringLDMToNewCode(learner, _excludedLDMs[0]);
            _excludedLDMs.RemoveAt(0);
        }
    }
}
