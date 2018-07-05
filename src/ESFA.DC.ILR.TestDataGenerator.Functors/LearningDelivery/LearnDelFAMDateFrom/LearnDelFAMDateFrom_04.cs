using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMDateFrom_04
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "LearnDelFAMDateFrom_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Ldfamfr04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, InvalidLines = 2 }
            };
        }

        private void Mutates(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery;
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in lds)
                {
                    foreach (MessageLearnerLearningDelivery mld in lds)
                    {
                        {
                            var ldfams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ACT.ToString());
                            ld.LearningDeliveryFAM = ldfams.ToArray();
                        }

                        var lfams = ld.LearningDeliveryFAM.ToList();
                        lfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                            LearnDelFAMDateFromSpecified = false
                        });
                        ld.LearningDeliveryFAM = lfams.ToArray();
                        break;
                    }
                }
            }
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            var lds = learner.LearningDelivery;
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                foreach (var ld in lds)
                {
                    foreach (MessageLearnerLearningDelivery mld in lds)
                    {
                        {
                            var ldfams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.ACT.ToString());
                            ld.LearningDeliveryFAM = ldfams.ToArray();
                        }
                    }
                }

                foreach (var ld in lds)
                {
                    var ldfams = ld.LearningDeliveryFAM.ToList();
                    ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                        LearnDelFAMDateFromSpecified = false
                    });
                    ld.LearningDeliveryFAM = ldfams.ToArray();
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
