﻿using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PriorAttain_04
        : ILearnerMultiMutator
    {
        private List<PriorAttainWithAppProgTypeAim> _attain;
        private ILearnerCreatorDataCache _cache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "PriorAttain_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PAtt_04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            _attain = new List<PriorAttainWithAppProgTypeAim>()
            {
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OldLevel4, PTA = _cache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OldLevel5, PTA = _cache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level4, PTA = _cache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level5, PTA = _cache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level6, PTA = _cache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level7, PTA = _cache.ApprenticeshipAims(ProgType.AdvancedLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OldLevel4, PTA = _cache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OldLevel5, PTA = _cache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level4, PTA = _cache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level5, PTA = _cache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level6, PTA = _cache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level7, PTA = _cache.ApprenticeshipAims(ProgType.IntermediateLevelApprenticeship).First() },
            };

            var result = new List<LearnerTypeMutator>();
            foreach (var v in _attain)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderFullyFunded(learner);
            Helpers.SetApprenticeshipAims(learner, _attain[0].PTA);
            if (!valid)
            {
                learner.PriorAttain = (int)_attain[0].Attain;
            }

            _attain.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeLDM = false;
            options.LD.IncludeHHS = true;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-APR-30");
        }

        private void MutateGenerationOptionsLDM(GenerationOptions options)
        {
        }
    }
}
