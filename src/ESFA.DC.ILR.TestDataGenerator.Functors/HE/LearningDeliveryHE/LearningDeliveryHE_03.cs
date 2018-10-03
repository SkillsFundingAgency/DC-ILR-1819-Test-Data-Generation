using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearningDeliveryHE_03 : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _cache;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "LearningDeliveryHE_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LDelHE03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateHE4, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = MutateHE4, DoMutateOptions = MutateUKPRNOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateHE5, DoMutateOptions = MutateUKPRNOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.NonFunded, DoMutateLearner = MutateHE6, DoMutateOptions = MutateUKPRNOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateHE7, DoMutateOptions = MutateUKPRNOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateLDMType, DoMutateOptions = MutateUKPRNOptions, ExclusionRecord = true },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Adult, DoMutateLearner = MutateUHEO, DoMutateOptions = MutateUHEOOptions, ExclusionRecord = true },
            };
        }

        public void MutateHE4(MessageLearner learner, bool valid)
        {
            MutateHE(learner, valid, "Z0006812");
        }

        public void MutateHE5(MessageLearner learner, bool valid)
        {
            MutateHE(learner, valid, "T5009975");
        }

        public void MutateHE6(MessageLearner learner, bool valid)
        {
            MutateHE(learner, valid, "ZO6XX153");
        }

        public void MutateHE7(MessageLearner learner, bool valid)
        {
            MutateHE(learner, valid, "Z0009082");
        }

        public void MutateUHEO(MessageLearner learner, bool valid)
        {
            MutateHE(learner, valid, "Z0009082");
        }

        public void MutateLDMType(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateHE(learner, valid, "Z0009082");
                var ld1Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.LDM_HESA_GeneratedILRfile).ToString()
                });
                learner.LearningDelivery[0].LearningDeliveryFAM = ld1Fams.ToArray();
            }
        }

        public void MutateHE(MessageLearner learner, bool valid, string aimref)
         {
                var hes = new List<MessageLearnerLearningDeliveryLearningDeliveryHE>(4);
                var Options = new GenerationOptions()
                {
                    LD = new LearningDeliveryOptions()
                    {
                        IncludeHEFields = true
                    }
                };
                var what = Options.LD.IncludeHEFields;
                if (valid)
                {
                    hes.Add(new MessageLearnerLearningDeliveryLearningDeliveryHE()
                    {
                        NUMHUS = "2000812012XTT60021",
                        QUALENT3 = QualificationOnEntry.X06.ToString(),
                        UCASAPPID = "AB89",
                        TYPEYR = (int)TypeOfyear.FEYear,
                        TYPEYRSpecified = true,
                        MODESTUD = (int)ModeOfStudy.NotInPopulation,
                        MODESTUDSpecified = true,
                        FUNDLEV = (int)FundingLevel.Undergraduate,
                        FUNDLEVSpecified = true,
                        FUNDCOMP = (int)FundingCompletion.NotYetCompleted,
                        FUNDCOMPSpecified = true,
                        STULOAD = 10.0M,
                        STULOADSpecified = true,
                        YEARSTU = 1,
                        YEARSTUSpecified = true,
                        MSTUFEE = (int)MajorSourceOfTuitionFees.NoAward,
                        MSTUFEESpecified = true,
                        PCFLDCS = 100,
                        PCFLDCSSpecified = true,
                        SPECFEE = (int)SpecialFeeIndicator.Other,
                        SPECFEESpecified = true,
                        NETFEE = 0,
                        NETFEESpecified = true,
                        GROSSFEE = 1,
                        GROSSFEESpecified = true,
                        DOMICILE = "ZZ",
                        ELQ = (int)EquivalentLowerQualification.NotRequired,
                        ELQSpecified = true
                    });
                }

                if (!valid)
                {
                learner.LearningDelivery[0].LearnAimRef = aimref;
                }
          }

        public void MutateLearnStartDate(MessageLearner learner, bool valid)
        {
            //MutateHE(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[0].LearnStartDate = DateTime.Parse("2010-AUG-01").AddDays(-1);
            }
        }

        public void MutateNoHE(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                learner.DateOfBirth = new DateTime(1998, 07, 01);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
        }

        private void MutateUKPRNOptions(GenerationOptions options)
        {
           options.OverrideUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.USFC).UKPRN;
        }

        private void MutateUHEOOptions(GenerationOptions options)
        {
            options.OverrideUKPRN = _cache.OrganisationWithLegalType(LegalOrgType.UHEO).UKPRN;
        }
    }
}
