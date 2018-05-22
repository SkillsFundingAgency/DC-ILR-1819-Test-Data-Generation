using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    /// <summary>
    /// The FM35 test data generator helps create a range of high quality test data specifically to test the FM35 funding model
    /// It starts with simple learners but becomes more and more complex in what the earning calculation has to do and areas where in the past there have been specific
    /// issues and bugs
    /// </summary>
    public class FM35_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;
        private DateTime _outcomeDate;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19LDPostcodeAreaCost, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19DisadvantagedPostcodeRate, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19DisadvantagedPostcodeRateWithAreaCost, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19FFI, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19LD3, DoMutateOptions = MutateGenerationOptionsLD3 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16Apprenticeship, DoMutateOptions = MutateGenerationOptionsOlderApprenticeship },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16ApprenticeshipCoFunded, DoMutateOptions = MutateGenerationOptionsOlderApprenticeship },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16ApprenticeshipCoFundedLDPostcodeAreaCost, DoMutateOptions = MutateGenerationOptionsOlderApprenticeship },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19ApprenticeshipCoFundedLDPostcodeAreaCost, DoMutateOptions = MutateGenerationOptionsOlderApprenticeship },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19ApprenticeshipCoFundedLDPostcodeAreaCostLDMATA, DoMutateOptions = MutateGenerationOptionsOlderApprenticeship },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16ApprenticeshipSimpleRestart, DoMutateOptions = MutateGenerationOptionsOlderApprenticeshipLD2 },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = Mutate19LD2Restarts, DoMutateOptions = MutateGenerationOptionsLD2, DoMutateProgression = Mutate19LD2RestartsDestAndProg },
//4)	Restarts.
//a.  Simple model
//i.  A-S-O….comp=6 with end date
//ii. J-F restart, prior learning %
//b.  Complex apprenticeship model
//i.  Break in ZPROG01
//ii. Two components, first finishes before end of zprog aim (so no achievement payment)
//iii.    The zprog + second then complete. The achievement payment for the 1st component should then appear when the zprog is achieved
            };
        }

        public string RuleName()
        {
            return "FM35_01";
        }

        public string LearnerReferenceNumberStub()
        {
            return "fm35_01";
        }

        private void Mutate19(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
        }

        private void Mutate19LDPostcodeAreaCost(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            learner.LearningDelivery[0].DelLocPostCode = _dataCache.PostcodeWithAreaCostFactor();
        }

        private void Mutate19DisadvantagedPostcodeRate(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            learner.PostcodePrior = _dataCache.PostcodeDisadvantagedArea();
        }

        private void Mutate19DisadvantagedPostcodeRateWithAreaCost(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            learner.LearningDelivery[0].DelLocPostCode = _dataCache.PostcodeWithAreaCostFactor();
            learner.PostcodePrior = _dataCache.PostcodeDisadvantagedArea();
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
        }

        private void Mutate19FFI(MessageLearner learner, bool valid)
        {
            MutateCommon(learner, valid);
            Helpers.RemoveLearningDeliveryFAM(learner, LearnDelFAMType.FFI);
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                var ld0Fams = ld.LearningDeliveryFAM.ToList();
                ld0Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString()
                });
                ld.LearningDeliveryFAM = ld0Fams.ToArray();
            }
        }

        private void Mutate19LD3(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            learner.LearningDelivery[1].LearnAimRef = "60126334";
        }

        private void Mutate16Apprenticeship(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Fully);
        }

        private void Mutate16ApprenticeshipCoFunded(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Co);
        }

        private void Mutate16ApprenticeshipCoFundedLDPostcodeAreaCost(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Co);
            learner.LearningDelivery[1].DelLocPostCode = _dataCache.PostcodeWithAreaCostFactor();
        }

        private void Mutate19ApprenticeshipCoFundedLDPostcodeAreaCost(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Co);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            learner.LearningDelivery[1].DelLocPostCode = _dataCache.PostcodeWithAreaCostFactor();
        }

        private void Mutate19ApprenticeshipCoFundedLDPostcodeAreaCostLDMATA(MessageLearner learner, bool valid)
        {
            Mutate19ApprenticeshipCoFundedLDPostcodeAreaCost(learner, valid);

            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();

            // create lots of LDM based LD FAMS
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_PrincesTrustTeamProgramme).ToString()
            });
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString()
            });
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_HESA_GeneratedILRfile).ToString()
            });
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ApprenticeshipTrainingAgency).ToString()
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
        }

        private void Mutate16ApprenticeshipSimpleRestart(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Co);
            var lds = learner.LearningDelivery.ToList();
            lds[0].LearnPlanEndDate = lds[0].LearnStartDate + TimeSpan.FromDays(365);
            lds[1].LearnActEndDate = lds[1].LearnStartDate + TimeSpan.FromDays(45);
            lds[1].LearnPlanEndDate = lds[0].LearnPlanEndDate;
            lds[1].LearnActEndDateSpecified = true;

            lds[1].CompStatus = (int)CompStatus.BreakInLearning;
            lds[1].Outcome = (int)Outcome.NoAchievement;
            lds[1].OutcomeSpecified = true;

            lds[2].LearnStartDate = lds[1].LearnActEndDate + TimeSpan.FromDays(30);
            Helpers.AddLearningDeliveryRestartFAM(lds[2]);
            lds[2].PriorLearnFundAdj = 80;
            lds[2].PriorLearnFundAdjSpecified = true;
            lds[2].LearnPlanEndDate = lds[0].LearnPlanEndDate;
//            Helpers.SetLearningDeliveryEndDates(lds[2], lds[0].LearnPlanEndDate, Helpers.SetAchDate.DoNotSetAchDate);
        }

        private void Mutate19LD2Restarts(MessageLearner learner, bool valid)
        {
            Mutate19(learner, valid);
            var lds = learner.LearningDelivery.ToList();
            lds[0].LearnActEndDate = lds[0].LearnStartDate + TimeSpan.FromDays(45);
            lds[0].LearnPlanEndDate = lds[0].LearnStartDate + TimeSpan.FromDays(75);
            lds[0].LearnActEndDateSpecified = true;

            lds[0].CompStatus = (int)CompStatus.BreakInLearning;
            lds[0].Outcome = (int)Outcome.NoAchievement;
            lds[0].OutcomeSpecified = true;

            lds[1].LearnStartDate = lds[0].LearnActEndDate + TimeSpan.FromDays(30);
            Helpers.AddLearningDeliveryRestartFAM(lds[1]);
            lds[1].PriorLearnFundAdj = 50;
            lds[1].PriorLearnFundAdjSpecified = true;
            lds[1].LearnPlanEndDate = lds[0].LearnPlanEndDate + TimeSpan.FromDays(45);
            lds[1].OrigLearnStartDate = lds[0].LearnStartDate;
            lds[1].OrigLearnStartDateSpecified = true;
            Helpers.SetLearningDeliveryEndDates(lds[1], lds[1].LearnPlanEndDate, Helpers.SetAchDate.DoNotSetAchDate);
            _outcomeDate = lds[1].LearnPlanEndDate;
        }

        private void Mutate19LD2RestartsDestAndProg(MessageLearnerDestinationandProgression learner, bool valid)
        {
            learner.DPOutcome[0].OutStartDate = _outcomeDate;
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            _options = options;
        }

        private void MutateGenerationOptionsLD3(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 3;
            _options = options;
        }

        private void MutateGenerationOptionsLD2(GenerationOptions options)
        {
            options.LD.GenerateMultipleLDs = 2;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-AUG-11");
            options.CreateDestinationAndProgression = true;
            _options = options;
        }

        private void MutateGenerationOptionsOlderApprenticeship(GenerationOptions options)
        {
            _options = options;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-APR-01");
            options.LD.IncludeHHS = true;
        }

        private void MutateGenerationOptionsOlderApprenticeshipLD2(GenerationOptions options)
        {
            _options = options;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-APR-01");
            options.LD.IncludeHHS = true;
            options.LD.GenerateMultipleLDs = 2;
        }
    }
}
