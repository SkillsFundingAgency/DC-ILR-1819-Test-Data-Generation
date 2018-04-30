using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class PriorAttain_07
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
            return "PriorAttain_07";
        }

        public string LearnerReferenceNumberStub()
        {
            return "PAtt_07";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            _attain = new List<PriorAttainWithAppProgTypeAim>()
            {
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OldLevel4, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OldLevel5, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level4, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level5, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level6, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.Level7, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.OtherNotKnown, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
                new PriorAttainWithAppProgTypeAim() { Attain = PriorAttain.NotKnown, PTA = _cache.ApprenticeshipAims(ProgType.Traineeship).First() },
            };

            var result = new List<LearnerTypeMutator>();
            foreach (var v in _attain)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, DoMutateProgression = MutateProgression });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            Helpers.MutateApprenticeToTrainee(learner, _cache);
            Helpers.SetApprenticeshipAims(learner, _attain[0].PTA);
            foreach (var ld in learner.LearningDelivery)
            {
                Helpers.SetLearningDeliveryEndDates(ld, ld.LearnStartDate.AddDays(25), ld.AimSeqNumber == 1 ? Helpers.SetAchDate.SetAchDate : Helpers.SetAchDate.DoNotSetAchDate);
            }

            if (!valid)
            {
                learner.PriorAttain = (int)_attain[0].Attain;
            }

            _attain.RemoveAt(0);
        }

        private static void SetApprenticeshipAims(MessageLearner learner, ApprenticeshipProgrammeTypeAim pta)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)pta.ProgType;
                ld.FworkCode = pta.FworkCode;
                if (ld.FworkCode == 0)
                {
                    ld.FworkCodeSpecified = false;
                }

                ld.PwayCode = pta.PwayCode;
                if (ld.PwayCode == 0)
                {
                    ld.PwayCodeSpecified = false;
                }
            }

            learner.LearningDelivery[1].LearnAimRef = pta.LearningDelivery.LearnAimRef;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeLDM = false;
            options.LD.IncludeHHS = true;
            options.LD.IncludeLDM = true;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2016-AUG-01");
            options.CreateDestinationAndProgression = true;
        }

        private void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid)
        {
            learner.DPOutcome[0].OutType = OutcomeType.EDU.ToString();
            learner.DPOutcome[0].OutCode = (int)OutcomeCode.EDU_Apprenticeship;
        }

        private void MutateGenerationOptionsLDM(GenerationOptions options)
        {
        }
    }
}
