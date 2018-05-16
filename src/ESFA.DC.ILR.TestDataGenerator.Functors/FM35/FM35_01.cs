using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class FM35_01
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
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

            //ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            //{
            //    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
            //    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_PrincesTrustTeamProgramme).ToString()
            //});
            //ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            //{
            //    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
            //    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString()
            //});
            //ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            //{
            //    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
            //    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_HESA_GeneratedILRfile).ToString()
            //});
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_ApprenticeshipTrainingAgency).ToString()
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
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

        private void MutateGenerationOptionsOlderApprenticeship(GenerationOptions options)
        {
            _options = options;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2017-APR-01");
            options.LD.IncludeHHS = true;
        }
    }
}
