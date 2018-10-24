using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R47 : ILearnerMultiMutator
    {
        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateRUI12, DoMutateOptions = MutateGenerationOptions },
            };
        }

        public void MutateRUI12(MessageLearner learner, bool valid)
        {
            learner.ContactPreference = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactCourses,
                    ContPrefCodeSpecified = true
                },
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactSurvey,
                    ContPrefCodeSpecified = true
                }
            }.ToArray();
            if (!valid)
            {
                learner.ContactPreference[0].ContPrefCode = 1;
                learner.ContactPreference[1].ContPrefCode = 1;
            }
        }

        public void MutateRUI4(MessageLearner learner, bool valid)
        {
            learner.ContactPreference = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactIllness,
                    ContPrefCodeSpecified = true
                }
            }.ToArray();
            if (!valid)
            {
                learner.ContactPreference[0].ContPrefType = "XXX";
                learner.ContactPreference[0].ContPrefCode = 7;
            }
        }

        public void MutateRUI5(MessageLearner learner, bool valid)
        {
            learner.ContactPreference = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactDead,
                    ContPrefCodeSpecified = true
                }
            }.ToArray();
            if (!valid)
            {
                learner.ContactPreference[0].ContPrefType = "XXX";
                learner.ContactPreference[0].ContPrefCode = 7;
            }
        }

        public void MutatePMC123(MessageLearner learner, bool valid)
        {
            var list = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.PMC.ToString(),
                    ContPrefCode = (int)ContactPrefCode.PMC_NotEmail,
                    ContPrefCodeSpecified = true
                },
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.PMC.ToString(),
                    ContPrefCode = (int)ContactPrefCode.PMC_NotPost,
                    ContPrefCodeSpecified = true
                },
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.PMC.ToString(),
                    ContPrefCode = (int)ContactPrefCode.PMC_NotPhone,
                    ContPrefCodeSpecified = true
                }
            };
            learner.ContactPreference = list.ToArray();
            if (!valid)
            {
                list.Add(new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.PMC.ToString(),
                    ContPrefCode = 7,
                    ContPrefCodeSpecified = true
                });
                learner.ContactPreference = list.ToArray();
                learner.ContactPreference[0].ContPrefType = "XXX";
                learner.ContactPreference[0].ContPrefCode = 8;
            }
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "R47";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R47";
        }
    }
}
