using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FD_AddLine2_AP
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
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Addline2_1, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Addline2_2, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Addline2_3, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public string RuleName()
        {
            return "FD_AddLine2_AP";
        }

        public string LearnerReferenceNumberStub()
        {
            return "FDAddLn2";
        }

        private char[] Mutate(bool valid)
        {
            char[] validChars =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', '@', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', '@', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                '~', '!', '&', '\\', '(', ')', '*', '+', ',', '-', '.', '/', ';', ':', '@'
            };

            char[] invalidChars =
            {
                '*', '$', '£', '#', '^', '%', '\"', '?', '>', '<', '|'
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

        private void Addline2_1(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var address = Helpers.GenerateString(49, Mutate(true));
            learner.AddLine2 = address;

            if (!valid)
            {
                var chars = Helpers.GenerateString(1, Mutate(valid));
                learner.AddLine2 = address + chars;
            }
        }

        private void Addline2_2(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var address = Helpers.GenerateString(10, Mutate(true));
            learner.AddLine2 = address;

            if (!valid)
            {
                var chars = Helpers.GenerateString(1, Mutate(valid));
                learner.AddLine2 = address + chars;
            }
        }

        private void Addline2_3(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            var address = Helpers.GenerateString(50, Mutate(true));
            learner.AddLine2 = address;

            if (!valid)
            {
                var chars = Helpers.GenerateString(1, Mutate(true));
                learner.AddLine2 = address + chars;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
