using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EmpOutcome_02
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "EmpOutcome_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "EmpOut_02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateESM, DoMutateOptions = MutateGenerationOptionsProgression, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateEmpOutcome, DoMutateOptions = MutateGenerationOptionsProgression, InvalidLines = 2, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsProgression, InvalidLines = 2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.ESF, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptionsProgression, InvalidLines = 2 },
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].EmpOutcomeSpecified = true;
            learner.LearningDelivery[1].EmpOutcomeSpecified = true;
            learner.LearningDelivery[0].EmpOutcome = 1;
            learner.LearningDelivery[1].EmpOutcome = 2;

            if (!valid)
            {
                learner.LearningDelivery[0].EmpOutcome = 7;
                learner.LearningDelivery[1].EmpOutcome = 8;
            }
        }

        private void MutateESM(MessageLearner learner, bool valid)
        {
            var lesm = learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring.ToList();

            lesm.Add(new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
            {
                ESMType = EmploymentStatusMonitoringType.BSI.ToString(),
                ESMCode = (int)EmploymentStatusMonitoringCode.BenefitEmploymentSupport,
                ESMCodeSpecified = true
            });
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring = lesm.ToArray();
            Mutate(learner, valid);
        }

        private void MutateEmpOutcome(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(1);
            if (!valid)
            {
                learner.LearningDelivery[0].EmpOutcomeSpecified = true;
                learner.LearningDelivery[1].EmpOutcomeSpecified = true;
                learner.LearningDelivery[0].EmpOutcome = 7;
                learner.LearningDelivery[1].EmpOutcome = 8;
            }
        }

        private void MutateGenerationOptionsProgression(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            options.CreateDestinationAndProgression = true;
        }
    }
}
