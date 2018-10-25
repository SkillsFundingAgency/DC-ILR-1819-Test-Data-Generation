using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FD_Sex_AP
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateSex1, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateSex2, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateSex3, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true } // AL error
            };
        }

        public string RuleName()
        {
            return "FD_Sex_AP";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FDSex";
        }

        private char[] Mutate(bool valid)
        {
            char[] validChars =
            {
                'F', 'M'
            };

            char[] invalidChars =
            {
                'm', 'f', '£', '#', '^', '%', '\"', '?', '>', '<', '|',
                'A', 'B', 'C', 'D', 'E', 'G', 'H', 'I', 'J', 'K', '@', 'L', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', '@', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                '~', '!', '&', '\\', '(', ')', '*', '+', ',', '-', '.', '/', ';', ':', '@'
            };

            if (valid)
            {
                return validChars;
            }
            else
            {
                return invalidChars;
            }
        }

        private void MutateSex1(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var sex = Helpers.GenerateString(1, Mutate(true));
            learner.Sex = sex;

            if (!valid)
            {
                learner.Sex = Helpers.GenerateString(1, Mutate(valid));
            }
        }

        private void MutateSex2(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var sex = Helpers.GenerateString(1, Mutate(true));
            learner.Sex = sex;

            if (!valid)
            {
                learner.Sex = Helpers.GenerateString(1, Mutate(valid));
            }
        }

        private void MutateSex3(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var sex = Helpers.GenerateString(1, Mutate(true));
            learner.Sex = sex;

            if (!valid)
            {
                learner.Sex = Helpers.GenerateString(1, Mutate(valid));
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
