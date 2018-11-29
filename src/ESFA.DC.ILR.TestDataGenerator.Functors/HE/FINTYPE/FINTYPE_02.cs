using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FINTYPE_02 :
        ILearnerMultiMutator
    {
        private readonly HashSet<int> _finType = new HashSet<int> { 1, 2, 3, 4 };

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "FINTYPE_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FINTY02";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType1, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType2, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType3, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinType4, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateFinTypes, DoMutateOptions = MutateGenerationOptionsHE, ExclusionRecord = true }
            };
        }

        public void Mutate(MessageLearner learner, bool valid, int finType)
        {
            var lhe = new List<MessageLearnerLearnerHE>();
            var lhefs = new List<MessageLearnerLearnerHELearnerHEFinancialSupport>();
            lhe.Add(new MessageLearnerLearnerHE()
                {
                    UCASPERID = "9999911111"
                });
            lhefs.Add(new MessageLearnerLearnerHELearnerHEFinancialSupport()
            {
                FINTYPESpecified = true,
                FINTYPE = finType,
                FINAMOUNTSpecified = true,
                FINAMOUNT = 99
            });
            learner.LearnerHE = lhe.ToArray();

            if (!valid)
            {
                lhefs.Add(new MessageLearnerLearnerHELearnerHEFinancialSupport()
                {
                    FINTYPESpecified = true,
                    FINTYPE = finType,
                    FINAMOUNTSpecified = true,
                    FINAMOUNT = 99
                });
            }

            learner.LearnerHE[0].LearnerHEFinancialSupport = lhefs.ToArray();
        }

        public void MutateFinType1(MessageLearner learner, bool valid)
        {
           Mutate(learner, valid, 1);
        }

        public void MutateFinType2(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid, 2);
        }

        public void MutateFinType3(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid, 3);
        }

        public void MutateFinType4(MessageLearner learner, bool valid)
        {
            Mutate(learner, valid, 4);
        }

        public void MutateFinTypes(MessageLearner learner, bool valid)
        {
            var lhefs = new List<MessageLearnerLearnerHELearnerHEFinancialSupport>();
            Mutate(learner, valid, 1);

            if (!valid)
            {
                lhefs.Add(new MessageLearnerLearnerHELearnerHEFinancialSupport()
                {
                    FINTYPESpecified = true,
                    FINTYPE = 1,
                    FINAMOUNTSpecified = true,
                    FINAMOUNT = 99
                });
                lhefs.Add(new MessageLearnerLearnerHELearnerHEFinancialSupport()
                {
                    FINTYPESpecified = true,
                    FINTYPE = 2,
                    FINAMOUNTSpecified = true,
                    FINAMOUNT = 99
                });
                lhefs.Add(new MessageLearnerLearnerHELearnerHEFinancialSupport()
                {
                    FINTYPESpecified = true,
                    FINTYPE = 3,
                    FINAMOUNTSpecified = true,
                    FINAMOUNT = 99
                });
                lhefs.Add(new MessageLearnerLearnerHELearnerHEFinancialSupport()
                {
                    FINTYPESpecified = true,
                    FINTYPE = 4,
                    FINAMOUNTSpecified = true,
                    FINAMOUNT = 99
                });
            }

            learner.LearnerHE[0].LearnerHEFinancialSupport = lhefs.ToArray();
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
            options.EmploymentRequired = true;
        }
    }
}
