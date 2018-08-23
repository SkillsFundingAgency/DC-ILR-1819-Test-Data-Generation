using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FINTYPE_01 :
        ILearnerMultiMutator
    {
        private readonly HashSet<int> _finType = new HashSet<int> { 1, 2, 3, 4 };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "FINTYPE_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FINTY01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType1, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType2, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType3, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType4, DoMutateOptions = MutateGenerationOptionsHE }
            };
        }

        public void Mutate(MessageLearner learner, bool valid, int finType)
        {
            var lhe = new List<MessageLearnerLearnerHE>();
            MessageLearnerLearnerHELearnerHEFinancialSupport mlhf = new MessageLearnerLearnerHELearnerHEFinancialSupport();
            mlhf.FINTYPESpecified = true;
            mlhf.FINTYPE = 1;
            lhe.Add(new MessageLearnerLearnerHE()
            {
                UCASPERID = "9999911111",
                LearnerHEFinancialSupport = new MessageLearnerLearnerHELearnerHEFinancialSupport()
                {
                    FINTYPESpecified = true,
                    FINTYPE = finType,
                    FINAMOUNTSpecified = true,
                    FINAMOUNT = 99
                },
            });
            learner.LearnerHE = lhe.ToArray();
        }

        public void MutateFinType1(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                Mutate(learner, valid, 1);
            }

            if (!valid)
            {
                Mutate(learner, valid, 5);
            }
        }

        public void MutateFinType2(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                Mutate(learner, valid, 2);
            }

            if (!valid)
            {
                Mutate(learner, valid, 6);
            }
        }

        public void MutateFinType3(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                Mutate(learner, valid, 3);
            }

            if (!valid)
            {
                Mutate(learner, valid, 7);
            }
        }

        public void MutateFinType4(MessageLearner learner, bool valid)
        {
            if (valid)
            {
                Mutate(learner, valid, 4);
            }

            if (!valid)
            {
                Mutate(learner, valid, 8);
            }
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.EmploymentRequired = true;
        }
    }
}
