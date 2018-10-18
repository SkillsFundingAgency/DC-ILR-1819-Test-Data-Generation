using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FD_FamilyName_AP
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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateFName1, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateFName2, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateFName3, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public string RuleName()
        {
            return "FD_FamilyName_AP";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FDFamName";
        }

        private char[] Mutate(bool valid)
        {
            char[] validChars =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '*', '$', '\\', '£'
            };

            char[] invalidChars =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                '\r', '\n', '\t', '\"', '|'
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

        private void MutateFName1(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var fName = Helpers.GenerateString(99, Mutate(true));
            learner.FamilyName = fName;

            if (!valid)
            {
                var chars = Helpers.GenerateString(1, Mutate(valid));
                learner.FamilyName = fName + chars;
            }
        }

        private void MutateFName2(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var fName = Helpers.GenerateString(9, Mutate(true));
            learner.FamilyName = fName;

            if (!valid)
            {
                var chars = Helpers.GenerateString(1, Mutate(valid));
                learner.FamilyName = fName + chars;
            }
        }

        private void MutateFName3(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var fName = Helpers.GenerateString(1, Mutate(true));
            learner.FamilyName = fName;

            if (!valid)
            {
                var chars = Helpers.GenerateString(100, Mutate(valid));
                learner.FamilyName = fName + chars;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
