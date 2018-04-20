using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_51
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate25Trainee, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate25Trainee, DoMutateOptions = MutateGenerationOptionsHERestart, ExclusionRecord = true }
            };
        }

        public string RuleName()
        {
            return "DateOfBirth_51";
        }

        public string LearnerReferenceNumberStub()
        {
            return "DOB_51";
        }

        private void Mutate25Trainee(MessageLearner learner, bool valid)
        {
            Mutate25(learner, valid);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.FworkCodeSpecified = false;
                ld.PwayCodeSpecified = false;
                ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(45);
            }
        }

        private void Mutate25(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|01]"));
            Helpers.MutateApprenticeToTrainee(learner, _dataCache);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less25, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.Older);
            foreach (var ld in learner.LearningDelivery)
            {
                ld.FundModel = (int)FundModel.YP1619;
                ld.FundModel = (int)FundModel.YP1619;
            }

            learner.LearningDelivery[1].AimType = (int)AimType.CoreAim1619;
            Helpers.RemoveLearningDeliveryFFIFAM(learner);
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.LD.IncludeHHS = true;
            options.EngMathsGardeRequired = true;
        }

        private void MutateGenerationOptionsHERestart(GenerationOptions options)
        {
            MutateGenerationOptionsHE(options);
            options.LD.IncludeRES = true;
        }
    }
}
