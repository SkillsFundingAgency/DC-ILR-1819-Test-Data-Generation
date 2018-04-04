using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_20
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "DOB_20";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherYP1619, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19Trainee, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true, InvalidLines = 2 }
            };
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            // add a new core aim as a copy and then mutate the old one to something else
            List<MessageLearnerLearningDelivery> lds = learner.LearningDelivery.ToList();
            MessageLearnerLearningDelivery newLd = lds[0].DeepClone();
            lds.Add(newLd);
            var lastld = lds.Last();
            lastld.LearnAimRef = "50023494";
            lastld.AimSeqNumber++;
            learner.LearningDelivery = lds.ToArray();

            Helpers.AddOrChangeSourceOfFunding(learner.LearningDelivery[0], LearnDelFAMCode.SOF_ESFA_Adult);
            learner.LearningDelivery[0].AimType = (int)AimType.StandAlone;

            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
        }

        private void Mutate19Trainee(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.SchoolAYStart, Helpers.MakeOlderOrYoungerWhenInvalid.Younger);
            Helpers.MutateApprenticeLearningDeliveryToTrainee(learner, _dataCache);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.LD.IncludeHHS = true;
        }
    }
}
