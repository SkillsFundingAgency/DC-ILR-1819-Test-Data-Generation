using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnAimRef_80
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LearnAimRef_80";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LAimR80";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptionsSpecialistCollege, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateAppreticeship, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateRestarts, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePriorAttain, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true }
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.LearnStartDate = new DateTime(2017, 08, 01).AddDays(-1);
                ld.LearnAimRef = "60110016";
            }
        }

        private void MutatePriorAttain(MessageLearner learner, bool valid)
        {
            MutateLearner(learner, valid);
            if (!valid)
            {
                learner.PriorAttainSpecified = true;
                learner.PriorAttain = (int)PriorAttain.Level4;
            }
        }

        private void MutateAppreticeship(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].ProgType = (int)ProgType.ApprenticeshipStandard;
            }
        }

        private void MutateRestarts(MessageLearner learner, bool valid)
        {
            MutatePriorAttain(learner, valid);
            if (!valid)
            {
                var ldfams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();

                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ldfams.ToArray();
            }
        }

        private void MutateGenerationOptionsSpecialistCollege(GenerationOptions options)
        {
            options.OverrideUKPRN = _dataCache.OrganisationWithLegalType(LegalOrgType.SpecialistDesignatedCollege).UKPRN;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
