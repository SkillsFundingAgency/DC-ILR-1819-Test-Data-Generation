using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class AFinType_10
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
            return "AFinType_10";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Afinty10";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateCommon, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppfinNull, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateAppFin, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateCommon, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateAppFin(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
                learner.LearningDelivery[0].ProgType = (int)pta.ProgType;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = (int)AimType.ProgrammeAim;
                var ld = learner.LearningDelivery[0];
                var ldfams = ld.LearningDeliveryFAM.ToList();
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TotalAssessmentPrice,
                    AFinCodeSpecified = true,
                    AFinDate = learner.LearningDelivery[0].LearnStartDate,
                    AFinDateSpecified = true
                });

                learner.LearningDelivery[0].AppFinRecord = appfin.ToArray();
            }
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            if (learner.LearningDelivery[0].FundModel == 99)
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-18).AddMonths(-3);
            }
            else
            {
                learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            }

            if (!valid)
            {
                ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
                learner.LearningDelivery[0].ProgType = (int)pta.ProgType;
                learner.LearningDelivery[0].ProgTypeSpecified = true;
                learner.LearningDelivery[0].AimType = (int)AimType.ProgrammeAim;
            }
        }

        private void MutateAppfinNull(MessageLearner learner, bool valid)
        {
            MutateAppFin(learner, valid);
            if (valid)
            {
                learner.LearningDelivery[0].AppFinRecord = null;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }
    }
}
