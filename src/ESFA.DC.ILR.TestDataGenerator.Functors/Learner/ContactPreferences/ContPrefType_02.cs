using System;
using System.Collections.Generic;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class ContPrefType_02 : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateRUI12, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateRUI3, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutatePMC123, DoMutateOptions = MutateGenerationOptions }
            };
        }

        public void MutateRUI12(MessageLearner learner, bool valid)
        {
            var list = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactIllness,
                    ContPrefCodeSpecified = true
                },
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactDead,
                    ContPrefCodeSpecified = true
                }
            };
            if (!valid)
            {
                list.Add(new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactSurvey,
                    ContPrefCodeSpecified = true
                });
                list.Add(new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactCourses,
                    ContPrefCodeSpecified = true
                });
            }

            learner.ContactPreference = list.ToArray();
        }

        public void MutateRUI3(MessageLearner learner, bool valid)
        {
            var list = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactOldDied,
                    ContPrefCodeSpecified = true
                }
            };
            learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2013-JUL-30");
            SetLearnAimRef(learner, valid);
            if (!valid)
            {
                list.Add(new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactSurvey,
                    ContPrefCodeSpecified = true
                });
                list.Add(new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactCourses,
                    ContPrefCodeSpecified = true
                });
            }

            learner.ContactPreference = list.ToArray();
        }

        public void MutatePMC123(MessageLearner learner, bool valid)
        {
            var list = new List<MessageLearnerContactPreference>()
            {
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactIllness,
                    ContPrefCodeSpecified = true
                },
                new MessageLearnerContactPreference()
                {
                    ContPrefType = ContactPrefType.RUI.ToString(),
                    ContPrefCode = (int)ContactPrefCode.RUI_NoContactDead,
                    ContPrefCodeSpecified = true
                }
            };
            if (!valid)
            {
                var newlist = new List<MessageLearnerContactPreference>()
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
                list.AddRange(newlist);
            }

            learner.ContactPreference = list.ToArray();
        }

        public void MutateGenerationOptions(GenerationOptions options)
        {
        }

        public string RuleName()
        {
            return "ContPrefType_02";
        }

        public string LearnerReferenceNumberStub()
        {
            return "ContPr_02";
        }

        private void SetLearnAimRef(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = _cache.LearnAimFundingWithValidity(
                (FundModel)learner.LearningDelivery[0].FundModel,
                LearnDelFAMCode.SOF_ESFA_1619,
                learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
        }
    }
}
