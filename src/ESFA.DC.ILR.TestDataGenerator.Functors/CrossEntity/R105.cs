using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R105
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "R105";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R105";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLD2Restart, DoMutateOptions = MutateGenerationOptionsLD2 }
            };
        }

        private void MutateLD2Restart(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
                var lds = learner.LearningDelivery.ToList();
                var ldfams = lds[1].LearningDeliveryFAM.ToList();
                    ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractESFA).ToString(),
                        LearnDelFAMDateFromSpecified = true,
                        LearnDelFAMDateFrom = DateTime.Now.AddMonths(-4),
                        LearnDelFAMDateToSpecified = true,
                        LearnDelFAMDateTo = DateTime.Now.AddMonths(-2)
                    });

                    lds[1].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            options.EmploymentRequired = true;
            _options = options;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
        }
    }
}
