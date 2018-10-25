using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnDelFAMType_04
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
            return "LearnDelFAMType_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LdfamTy04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateSOF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateFFI, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateEEF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateRES, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLSF, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateADL, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateALB, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateASL, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateFLN, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDM, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateNSA, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutatePOD, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHHS, DoMutateOptions = MutateGenerationOptionsHHS },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateACT, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHEM, DoMutateOptions = MutateGenerationOptionsHE },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateWPP, DoMutateOptions = MutateGenerationOptions }
            };
        }

        private void MutateSOF(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.SOF, LearnDelFAMCode.SOF_ESFA_Adult);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.SOF, LearnDelFAMCode.FFI_Co);
            }
        }

        private void MutateASL(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-18).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.ASL, LearnDelFAMCode.ASL_Personal);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.ASL, LearnDelFAMCode.SOF_ESFA_Adult);
            }
        }

        private void MutateFFI(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FFI, LearnDelFAMCode.SOF_ESFA_Adult);
            }
        }

        private void MutateEEF(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.EEF_Apprenticeship_19);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.EEF, LearnDelFAMCode.SOF_ESFA_Adult);
            }
        }

        private void MutateRES(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.RES, LearnDelFAMCode.RES);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.RES, LearnDelFAMCode.SOF_ESFA_Adult);
            }
        }

        private void MutateLSF(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LSF).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }
            else if (!valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LSF.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SolentCity).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }

            ld.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateADL(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-1);
            learner.LearningDelivery[0].LearnAimRef = _dataCache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, learner.LearningDelivery[0].LearnStartDate).LearnAimRef;
            if (valid)
            {
              Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.ADL, LearnDelFAMCode.ADL);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.ADL, LearnDelFAMCode.LDM_SteelRedundancy);
            }
        }

        private void MutateALB(MessageLearner learner, bool valid)
        {
            var ld = learner.LearningDelivery[0];
            var ldfams = ld.LearningDeliveryFAM.ToList();
            ld.LearnAimRef = "6030599X";
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-1);
            if (valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.ALB_Rate_1).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }

            if (!valid)
            {
                ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ALB.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString(),
                    LearnDelFAMDateFrom = ld.LearnStartDate,
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateTo = ld.LearnPlanEndDate,
                    LearnDelFAMDateToSpecified = true
                });
            }

            ldfams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.ADL.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.ADL).ToString()
            });

            ld.LearningDeliveryFAM = ldfams.ToArray();
        }

        private void MutateFLN(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FLN, LearnDelFAMCode.FLN_FEML);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.FLN, LearnDelFAMCode.LDM_SteelRedundancy);
            }
        }

        private void MutateLDM(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.LDM_SteelRedundancy);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.LDM, LearnDelFAMCode.SOF_Other);
            }
        }

        private void MutateNSA(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2016, 07, 31).AddDays(-1);
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.NSA, LearnDelFAMCode.NSA_Socialcare);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.NSA, LearnDelFAMCode.LDM_Military);
            }
        }

        private void MutatePOD(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.POD, LearnDelFAMCode.POD_FItyPercent);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.POD, LearnDelFAMCode.LDM_Military);
            }
        }

        private void MutateHHS(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);

            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.HHS);
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HHS, LearnDelFAMCode.SOF_ESFA_Adult);
            }
        }

        private void MutateACT(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.ACT);
                Helpers.SafeAddlearningDeliveryFAM(learner.LearningDelivery[0], LearnDelFAMType.ACT, LearnDelFAMCode.HHS_NoneAbove, learner.LearningDelivery[0].LearnStartDate, learner.LearningDelivery[0].LearnPlanEndDate);
            }
        }

        private void MutateHEM(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-1);

            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HEM, LearnDelFAMCode.HEM_Award);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.HEM, LearnDelFAMCode.ASL_WiderFamily);
            }
        }

        private void MutateWPP(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.WPP, LearnDelFAMCode.WPP_DWP);
            }

            if (!valid)
            {
                Helpers.AddLearningDeliveryFAM(learner, LearnDelFAMType.WPP, LearnDelFAMCode.SOF_LA);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateGenerationOptionsHHS(GenerationOptions options)
        {
            options.LD.IncludeHHS = true;
        }

        private void MutateGenerationOptionsHE(GenerationOptions options)
        {
            options.LD.IncludeHEFields = true;
        }
    }
}
