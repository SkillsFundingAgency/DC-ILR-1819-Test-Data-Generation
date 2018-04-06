using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_50
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_50";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
        //    AdvancedLevelApprenticeship = 2,
        //IntermediateLevelApprenticeship = 3,
        //HigherApprenticeshipLevel4 = 20,
        //HigherApprenticeshipLevel5 = 21,
        //HigherApprenticeshipLevel6 = 22,
        //HigherApprenticeshipLevel7 = 23,
        //Traineeship = 24,
        //ApprenticeshipStandard = 25
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16Trainee, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true }
            };
        }

        private void Mutate16Trainee(MessageLearner learner, bool valid)
        {
            Mutate16(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.FworkCodeSpecified = false;
                ld.PwayCodeSpecified = false;
                ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(45);
            }
        }

        private void Mutate16(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|01]"));
            Helpers.MutateApprenticeLearningDeliveryToTrainee(learner, _dataCache);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less16And30Days, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.YoungerLots);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.FundModel = (int)FundModel.YP1619;
                ld.FundModel = (int)FundModel.YP1619;
            }

            learner.LearningDelivery[1].AimType = (int)AimType.CoreAim1619;
            Helpers.RemoveFFIFromLearningDelivery(learner);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.LD.IncludeHHS = true;
            options.EngMathsGardeRequired = true;
        }
    }
}
