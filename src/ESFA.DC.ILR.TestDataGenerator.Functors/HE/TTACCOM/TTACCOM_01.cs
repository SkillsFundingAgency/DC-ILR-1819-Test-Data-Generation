using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class TTACCOM_01 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "TTACCOM_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "TTACCOM01";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM1, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM2, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM4, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM5, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM6, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM7, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM8, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateTTACCOM9, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateNoHE, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        public void MutateTTACCOM(MessageLearner learner, bool valid, int ttaccom)
        {
            var lhe = new List<MessageLearnerLearnerHE>();
            if (valid)
            {
                lhe.Add(new MessageLearnerLearnerHE()
                {
                    TTACCOMSpecified = true,
                    TTACCOM = ttaccom
                });
            }

            if (!valid)
            {
                lhe.Add(new MessageLearnerLearnerHE()
                {
                    TTACCOMSpecified = true,
                    TTACCOM = 0
                });
            }

            learner.LearnerHE = lhe.ToArray();
        }

        public void MutateTTACCOM1(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 1);
        }

        public void MutateTTACCOM2(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 2);
        }

        public void MutateTTACCOM4(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 4);
        }

        public void MutateTTACCOM5(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 5);
        }

        public void MutateTTACCOM6(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 6);
        }

        public void MutateTTACCOM7(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 7);
        }

        public void MutateTTACCOM8(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 8);
        }

        public void MutateTTACCOM9(MessageLearner learner, bool valid)
        {
            MutateTTACCOM(learner, valid, 9);
        }

        public void MutateNoHE(MessageLearner learner, bool valid)
        {
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
        }
    }
}
