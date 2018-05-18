using System;
using System.Collections.Generic;
using DCT.ILR.Model;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

namespace DCT.TestDataGenerator
{
    internal class LearnerGenerator
    {
        private ILearnerCreatorDataCache _cache;

        public LearnerGenerator(ILearnerCreatorDataCache cache)
        {
            _cache = cache;
        }

        public GenerationOptions Options { get; set; }

        public GenerationOptions CreateGenerationOptions(LearnerTypeRequired requiresLearner)
        {
            switch (requiresLearner)
            {
                case LearnerTypeRequired.CommunityLearning:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        NIRequired = false,
                        AimDefaultType = GenerationOptions.AimTypes.CommunityLearningFM10ValidAims,
                        Age19 = true,
                        EmploymentRequired = true
                    };
                    break;
                case LearnerTypeRequired.Apprenticeships:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        NIRequired = true,
                        Age19 = false,
                        EmploymentRequired = true,
                        LD = new LearningDeliveryOptions()
                        {
                            IncludeSOF = true
                        }
                    };
                    break;
                case LearnerTypeRequired.NonFunded:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        NIRequired = false,
                        AimDefaultType = GenerationOptions.AimTypes.NonfundedValidAims,
                        Age19 = true
                    };
                    break;
                case LearnerTypeRequired.YP1619:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        NIRequired = true,
                        AimDefaultType = GenerationOptions.AimTypes.YP1619Aims,
                        EngMathsGardeRequired = true,
                        EmploymentRequired = true,
                        LD = new LearningDeliveryOptions()
                        {
                            IncludeSOF = true
                        }
                    };
                    break;
                case LearnerTypeRequired.OtherYP1619:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        NIRequired = true,
                        AimDefaultType = GenerationOptions.AimTypes.OtherYP1619Aims,
                        EngMathsGardeRequired = true,
                        EmploymentRequired = true,
                        LD = new LearningDeliveryOptions()
                        {
                            IncludeSOF = true,
                            IncludeLDM = true
                        }
                    };
                    break;
                case LearnerTypeRequired.Adult:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        Age19 = true,
                        NIRequired = true,
                        AimDefaultType = GenerationOptions.AimTypes.AdultFM35ValidAims,
                        EmploymentRequired = true,
                        LD = new LearningDeliveryOptions()
                        {
                            IncludeSOF = true,
                            IncludeFFI = true,
                            IncludeHHS = true
                        }
                    };
                    break;
                case LearnerTypeRequired.OtherAdult:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        Age19 = true,
                        NIRequired = true,
                        AimDefaultType = GenerationOptions.AimTypes.OtherAdultFM81ValidAims,
                        EmploymentRequired = true,
                        LD = new LearningDeliveryOptions()
                        {
                            IncludeSOF = true,
                            IncludeFFI = true,
                            IncludeHHS = true,
                            IncludeLDM = true
                        }
                    };
                    break;
                case LearnerTypeRequired.ESF:
                    this.Options = new GenerationOptions()
                    {
                        DOBRequired = true,
                        Age19 = true,
                        NIRequired = true,
                        AimDefaultType = GenerationOptions.AimTypes.ESFValidAims,
                        EmploymentRequired = true,
                        LD = new LearningDeliveryOptions()
                        {
                            IncludeSOF = true,
                            IncludeFFI = false,
                            IncludeHHS = true,
                            IncludeContract = true
                        }
                    };
                    break;
                default:
                    throw new NotImplementedException($"CreateGenerationOptions for {requiresLearner} has not been implemented");
            }

            return this.Options;
        }

        internal MessageLearnerDestinationandProgression GenerateProgression(string ruleName, MessageLearner s)
        {
            return new MessageLearnerDestinationandProgression()
            {
                LearnRefNumber = s.LearnRefNumber,
                ULN = s.ULN,
                ULNSpecified = s.ULNSpecified,
                DPOutcome = new List<MessageLearnerDestinationandProgressionDPOutcome>()
                {
                    new MessageLearnerDestinationandProgressionDPOutcome()
                    {
                        OutType = OutcomeType.VOL.ToString(),
                        OutCode = (int)OutcomeCode.VOL_Work,
                        OutCodeSpecified = true,
                        OutStartDate = s.LearningDelivery[0].LearnActEndDateSpecified ? s.LearningDelivery[0].LearnActEndDate : s.LearningDelivery[0].LearnStartDate,
                        OutStartDateSpecified = true,
                        OutCollDate = s.LearningDelivery[0].LearnStartDate,
                        OutCollDateSpecified = true
                    }
                }.ToArray()
            };
        }

        internal List<MessageLearner> Generate(string learnerRefNumberStub, LearnerTypeRequired requiresLearner, int currentLearnerIndex, ref long ULNIndex)
        {
//            ULNIndex = currentLearnerIndex;
            long uln = 0;
            bool ok = false;
            while (!ok)
            {
                try
                {
                    uln = NextULN(ULNIndex);
                    ok = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                }

                ++ULNIndex;
            }

            List<MessageLearner> result = new List<MessageLearner>();
            MessageLearner learner = new MessageLearner()
            {
                LearnRefNumber = $"{currentLearnerIndex:X}{learnerRefNumberStub.Replace("_", string.Empty)}",
                ULN = uln,
                ULNSpecified = true
            };
            if (learner.LearnRefNumber.Length > _cache.MaximumLearnRefLength())
            {
                throw new ArgumentOutOfRangeException($"{learner.LearnRefNumber} is too long ({learner.LearnRefNumber.Length}) (max is {_cache.MaximumLearnRefLength()})");
            }

            PopulateLearnerForRules(learner);

            result.Add(learner);
            return result;
        }

        private void PopulateLearnerForRules(MessageLearner learner)
        {
            learner.Ethnicity = (int)Ethnicity.WhiteBritish;
            learner.EthnicitySpecified = true;
            learner.Sex = Sex.M.ToString();
            learner.LLDDHealthProb = (int)LLDDHealthProb.NoLearningDifficultOrHealthProblem;
            learner.LLDDHealthProbSpecified = true;

            learner.AddLine1 = "18 Address line road";
            learner.GivenNames = "Mary Jane";
            learner.FamilyName = "Sméth";
            learner.PostcodePrior = "ZZ99 9ZZ";
            learner.Postcode = "ZZ99 9ZZ";

            learner.TelNo = "07855555555";
            learner.Email = "myemail@myemail.com";
            learner.PlanLearnHours = 90;
            learner.PlanLearnHoursSpecified = true;
            learner.PriorAttain = (int)PriorAttain.Level2;
            learner.PriorAttainSpecified = true;
            learner.PlanEEPHours = 1;
            learner.PlanEEPHoursSpecified = true;
            if (Options.NIRequired)
            {
                learner.NINumber = "LJ000000A";
            }

            if (Options.DOBRequired)
            {
                learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[Age|18]"));
                learner.DateOfBirthSpecified = true;
                if (Options.Age19)
                {
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[Age|20]"));
                }
            }

            if (Options.EngMathsGardeRequired)
            {
                learner.MathGrade = "NONE";
                learner.EngGrade = "NONE";
            }

            if (Options.AccomodationRequired)
            {
                learner.Accom = (int)Accomodation.AwayFromHome;
                learner.AccomSpecified = true;
            }

            GenerateLearnerLLDDHealthProblem(learner);
            GenerateLearnerFAMs(learner);
            GenerateLearningDelivery(learner);
            GenerateLearnerEmploymentStatus(learner);
            GenerateProviderLearnerSpecMonitoring(learner);
        }

        private void GenerateLearnerLLDDHealthProblem(MessageLearner learner)
        {
            if (Options.LLDDHealthProblemRequired)
            {
                learner.LLDDandHealthProblem = new List<MessageLearnerLLDDandHealthProblem>()
                {
                    new MessageLearnerLLDDandHealthProblem()
                    {
                        LLDDCat = (int)LLDDCat.Dyslexia,
                        LLDDCatSpecified = true,
                        PrimaryLLDD = 0,
                        PrimaryLLDDSpecified = false
                    },
                    new MessageLearnerLLDDandHealthProblem()
                    {
                        LLDDCat = (int)LLDDCat.Aspergers,
                        LLDDCatSpecified = true,
                        PrimaryLLDD = 1,
                        PrimaryLLDDSpecified = true
                    },
                    new MessageLearnerLLDDandHealthProblem()
                    {
                        LLDDCat = (int)LLDDCat.HearingImpairement,
                        LLDDCatSpecified = true,
                        PrimaryLLDD = 0,
                        PrimaryLLDDSpecified = false
                    }
                }.ToArray();
            }
        }

        private void GenerateProviderLearnerSpecMonitoring(MessageLearner learner)
        {
            if (Options.ProviderSpecialMonitoringRequired)
            {
                learner.ProviderSpecLearnerMonitoring = new List<MessageLearnerProviderSpecLearnerMonitoring>()
                {
                    new MessageLearnerProviderSpecLearnerMonitoring()
                    {
                        ProvSpecLearnMon = $"{learner.ULN}",
                        ProvSpecLearnMonOccur = ProvSpecLearnMonOccur.A.ToString()
                    },
                    new MessageLearnerProviderSpecLearnerMonitoring()
                    {
                        ProvSpecLearnMon = $"{learner.ULN}",
                        ProvSpecLearnMonOccur = ProvSpecLearnMonOccur.B.ToString()
                    },
                }.ToArray();
            }
        }

        private void GenerateLearnerFAMs(MessageLearner learner)
        {
            List<MessageLearnerLearnerFAM> fams = new List<MessageLearnerLearnerFAM>(4);
            if (Options.FAM.HighNeedsStudentRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.HNS.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.HNS_Yes,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.EducationHealthCarePlanRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.EHC.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.EHC_Yes,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.EFADisadvantageFundingRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.EDF.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.EDF_MathsNotGot,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.MathsConditionOfFundingRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.MCF.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.MCF_ExcemptLearningDifficulty,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.EnglishConditionOfFundingRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.ECF.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.ECF_ExcemptLearningDifficulty,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.SpecialEducationalNeedsRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.SEN.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.SEN_Yes,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.LearnerSupportRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.LSR.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.LSR_Residential,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.FreeMealEntitlementRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.FME.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.FME_1619Receipt,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.DisabledLearnerAllowanceRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.DLA.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.DLA_Yes,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.NationalLearnerMonitoringRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.NLM.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.NLM_ContractLevel,
                    LearnFAMCodeSpecified = true
                });
            }

            if (Options.FAM.PupilPremiumEligibilityRequired)
            {
                fams.Add(new MessageLearnerLearnerFAM()
                {
                    LearnFAMType = LearnerFAMType.PPE.ToString(),
                    LearnFAMCode = (int)LearnerFAMCode.PPE_ServiceChild,
                    LearnFAMCodeSpecified = true
                });
            }

            learner.LearnerFAM = fams.ToArray();
        }

        private long NextULN(long index)
        {
            return ListOfULNs.ULN(index);
        }

        private void GenerateLearnerEmploymentStatus(MessageLearner messageLearner)
        {
            if (Options.EmploymentRequired)
            {
                MessageLearnerLearnerEmploymentStatus les = new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = (int)EmploymentStatus.PaidEmployment,
                    EmpStatSpecified = true,
                    DateEmpStatApp = Options.LD.OverrideLearnStartDate.HasValue ? Options.LD.OverrideLearnStartDate.Value.AddDays(-40) : DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY-1|JUN|10]")),
                    DateEmpStatAppSpecified = true,
                    EmpId = 154549452,
                    EmpIdSpecified = true,
                    EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                    {
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                            ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                            ESMCodeSpecified = true
                        },
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMType = EmploymentStatusMonitoringType.LOE.ToString(),
                            ESMCode = (int)EmploymentStatusMonitoringCode.Employed12Plus,
                            ESMCodeSpecified = true
                        }
                    }
                };
                messageLearner.LearnerEmploymentStatus = new List<MessageLearnerLearnerEmploymentStatus>() { les }.ToArray();
            }
        }

        private void GenerateLearningDelivery(MessageLearner messageLearner)
        {
            List<MessageLearnerLearningDelivery> aims = new List<MessageLearnerLearningDelivery>();
            if (Options.AimDefaultType == GenerationOptions.AimTypes.ApprenticeshipFM36ValidAims)
            {
                aims.Add(GenerateZProgLD());
                aims.AddRange(GenerateComponentLD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.CommunityLearningFM10ValidAims)
            {
                aims.AddRange(GenerateCommunityLearningLD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.NonfundedValidAims)
            {
                aims.AddRange(GenerateNonFundedLD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.YP1619Aims)
            {
                aims.AddRange(GenerateYP1619LD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.OtherYP1619Aims)
            {
                aims.AddRange(GenerateOtherYP1619LD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.AdultFM35ValidAims)
            {
                aims.AddRange(GenerateAdultLD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.OtherAdultFM81ValidAims)
            {
                aims.AddRange(GenerateOtherAdultLD());
            }
            else if (Options.AimDefaultType == GenerationOptions.AimTypes.ESFValidAims)
            {
                aims.Add(GenerateZESF0001LD());
                aims.AddRange(GenerateESFLD());
            }
            else
            {
                throw new NotImplementedException($"AimDefaultType is {Options.AimDefaultType} is not supported");
            }

            messageLearner.LearningDelivery = aims.ToArray();
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateCommunityLearningLD()
        {
            int required = Math.Max(1, Options.LD.GenerateMultipleLDs);
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>(required);
            for (int i = 0; i != required; ++i)
            {
                MessageLearnerLearningDelivery ld = new MessageLearnerLearningDelivery();

                ld.AimTypeSpecified = true;
                ld.FundModel = (int)FundModel.CommunityLearning;
                ld.FundModelSpecified = true;
                ld.LearnAimRef = "60021238";
                ld.AimType = (long)AimType.StandAlone;

                ld.AimSeqNumber = i + 1;
                ld.AimSeqNumberSpecified = true;
                ld.LearnStartDate = DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY|OCT|14]"));
                if (Options.LD.OverrideLearnStartDate.HasValue)
                {
                    ld.LearnStartDate = Options.LD.OverrideLearnStartDate.Value;
                }

                ld.LearnStartDateSpecified = true;

                ld.LearnPlanEndDate = AYPlus1July31();
                ld.LearnPlanEndDateSpecified = true;
                ld.DelLocPostCode = "ZZ99 9ZZ";
                ld.CompStatus = (long)CompStatus.Continuing;
                ld.CompStatusSpecified = true;

                if (Options.LD.IncludeContract)
                {
                    ld.ConRefNumber = _cache.ESFContractNumber();
                }

                if (Options.LD.IncludeOutcome)
                {
                    ld.Outcome = (int)Outcome.Achieved;
                    ld.OutcomeSpecified = true;
                }

                ld.LearningDeliveryFAM = LDFAMs(ld);
                ld.LearningDeliveryHE = LDHEs(ld);
                result.Add(ld);
            }

            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateNonFundedLD()
        {
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>();
            result.AddRange(GenerateCommunityLearningLD());
            result.ForEach(s => s.FundModel = (int)FundModel.NonFunded);
            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateYP1619LD()
        {
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>();
            result.AddRange(GenerateCommunityLearningLD());
            result.ForEach(s =>
                {
                    s.FundModel = (int)FundModel.YP1619;
                    s.AimType = (int)AimType.CoreAim1619;
            });
            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateOtherYP1619LD()
        {
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>();
            result.AddRange(GenerateCommunityLearningLD());
            result.ForEach(s =>
            {
                s.FundModel = (int)FundModel.OtherYP1619;
                s.AimType = (int)AimType.CoreAim1619;
            });
            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateAdultLD()
        {
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>();
            result.AddRange(GenerateCommunityLearningLD());
            result.ForEach(s => s.FundModel = (int)FundModel.Adult);
            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateOtherAdultLD()
        {
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>();
            result.AddRange(GenerateCommunityLearningLD());
            result.ForEach(s => s.FundModel = (int)FundModel.OtherAdult);
            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateESFLD()
        {
            int sequence = 2;
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>();
            result.AddRange(GenerateCommunityLearningLD());
            result.ForEach(s =>
            {
                s.FundModel = (int)FundModel.ESF;
                s.AimSeqNumber = sequence++;
            });
            return result;
        }

        private IEnumerable<MessageLearnerLearningDelivery> GenerateComponentLD()
        {
            int required = Math.Max(1, Options.LD.GenerateMultipleLDs);
            List<MessageLearnerLearningDelivery> result = new List<MessageLearnerLearningDelivery>(required);
            for (int i = 0; i != required; ++i)
            {
                MessageLearnerLearningDelivery ld = new MessageLearnerLearningDelivery();

                ld.AimTypeSpecified = true;
                ld.FundModel = (int)FundModel.Apprenticeships;
                ld.FundModelSpecified = true;
                ld.LearnAimRef = "50104767";
                ld.AimType = (long)AimType.ComponentAim;

                ld.AimSeqNumber = i + 2;
                ld.AimSeqNumberSpecified = true;
                ld.LearnStartDate = DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY|OCT|14]"));
                if (Options.LD.OverrideLearnStartDate.HasValue)
                {
                    ld.LearnStartDate = Options.LD.OverrideLearnStartDate.Value;
                }

                ld.LearnStartDateSpecified = true;

                ld.LearnPlanEndDate = AYPlus1July31();
                ld.LearnPlanEndDateSpecified = true;
                ld.DelLocPostCode = "ZZ99 9ZZ";
                ld.CompStatus = (long)CompStatus.Continuing;
                ld.CompStatusSpecified = true;

                ld.FworkCode = 420;
                ld.FworkCodeSpecified = true;
                ld.PwayCode = 1;
                ld.PwayCodeSpecified = true;
                ld.ProgType = (long)ProgType.AdvancedLevelApprenticeship;
                ld.ProgTypeSpecified = true;
                ld.LearningDeliveryFAM = LDFAMs(ld);
                ld.LearningDeliveryHE = LDHEs(ld);

                result.Add(ld);
            }

            return result;
        }

        private MessageLearnerLearningDelivery GenerateZESF0001LD()
        {
            MessageLearnerLearningDelivery ld = new MessageLearnerLearningDelivery();
            ld.AimTypeSpecified = true;
            ld.FundModel = (int)FundModel.ESF;
            ld.FundModelSpecified = true;
            ld.LearnAimRef = "ZESF0001";
            ld.AimType = (long)AimType.StandAlone;

            ld.AimSeqNumber = 1;
            ld.AimSeqNumberSpecified = true;
            ld.LearnStartDate = DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY|OCT|14]"));
            if (Options.LD.OverrideLearnStartDate.HasValue)
            {
                ld.LearnStartDate = Options.LD.OverrideLearnStartDate.Value;
            }

            ld.LearnStartDateSpecified = true;

            ld.LearnPlanEndDate = AYPlus1July31();
            ld.LearnPlanEndDateSpecified = true;
            ld.DelLocPostCode = "ZZ99 9ZZ";
            ld.CompStatus = (long)CompStatus.Continuing;
            ld.CompStatusSpecified = true;
            if (Options.LD.IncludeContract)
            {
                ld.ConRefNumber = _cache.ESFContractNumber();
            }

            ld.LearningDeliveryFAM = LDFAMs(ld);

            return ld;
        }

        private MessageLearnerLearningDelivery GenerateZProgLD()
        {
            MessageLearnerLearningDelivery ld = new MessageLearnerLearningDelivery();
            ld.AimTypeSpecified = true;
            ld.FundModel = (int)FundModel.Apprenticeships;
            ld.FundModelSpecified = true;
            ld.LearnAimRef = "ZPROG001";
            ld.AimType = (long)AimType.ProgrammeAim;

            ld.AimSeqNumber = 1;
            ld.AimSeqNumberSpecified = true;
            ld.LearnStartDate = DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY|OCT|14]"));
            if (Options.LD.OverrideLearnStartDate.HasValue)
            {
                ld.LearnStartDate = Options.LD.OverrideLearnStartDate.Value;
            }

            ld.LearnStartDateSpecified = true;

            ld.LearnPlanEndDate = AYPlus1July31();
            ld.LearnPlanEndDateSpecified = true;
            ld.DelLocPostCode = "ZZ99 9ZZ";
            ld.CompStatus = (long)CompStatus.Continuing;
            ld.CompStatusSpecified = true;

            ld.FworkCode = 420;
            ld.FworkCodeSpecified = true;
            ld.PwayCode = 1;
            ld.PwayCodeSpecified = true;
            ld.ProgType = (long)ProgType.AdvancedLevelApprenticeship;
            ld.ProgTypeSpecified = true;
            ld.LearningDeliveryFAM = LDFAMs(ld);
            ld.AppFinRecord = LDAppFinancialRecord(ld);

            return ld;
        }

        private MessageLearnerLearningDeliveryAppFinRecord[] LDAppFinancialRecord(MessageLearnerLearningDelivery ld)
        {
            List<MessageLearnerLearningDeliveryAppFinRecord> result = new List<MessageLearnerLearningDeliveryAppFinRecord>();
            LearnDelAppFinCode[] code = { LearnDelAppFinCode.TotalTrainingPrice, LearnDelAppFinCode.TotalAssessmentPrice, LearnDelAppFinCode.TrainingPayment, LearnDelAppFinCode.TrainingPayment };
            DateTime[] date = { ld.LearnStartDate, ld.LearnStartDate, ld.LearnStartDate + TimeSpan.FromDays(30), ld.LearnStartDate + TimeSpan.FromDays(30) };
            long[] amount = { 1200, 59, 110, 98 };
            LearnDelAppFinType[] type = { LearnDelAppFinType.TNP, LearnDelAppFinType.TNP, LearnDelAppFinType.PMR, LearnDelAppFinType.PMR };
            switch (Options.AimDefaultType)
            {
                case GenerationOptions.AimTypes.ApprenticeshipFM36ValidAims:
                    result.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                    {
                        AFinType = type[0].ToString(),
                        AFinCode = (long)code[0],
                        AFinAmount = amount[0],
                        AFinAmountSpecified = true,
                        AFinCodeSpecified = true,
                        AFinDate = date[0],
                        AFinDateSpecified = true
                    });
                    break;
            }

            return result.ToArray();
        }

        private MessageLearnerLearningDeliveryLearningDeliveryFAM[] LDFAMs(MessageLearnerLearningDelivery ld)
        {
            List<MessageLearnerLearningDeliveryLearningDeliveryFAM> fams = new List<MessageLearnerLearningDeliveryLearningDeliveryFAM>(4);
            switch (Options.AimDefaultType)
            {
                case GenerationOptions.AimTypes.ApprenticeshipFM36ValidAims:
                    if (ld.AimSeqNumber == 1)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.ACT.ToString(),
                            LearnDelFAMCode = Options.SetACTToFullyFunded ? ((int)LearnDelFAMCode.ACT_ContractESFA).ToString() : ((int)LearnDelFAMCode.ACT_ContractEmployer).ToString(),
                            LearnDelFAMDateFrom = ld.LearnStartDate,
                            LearnDelFAMDateFromSpecified = true
                        });
                    }

                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                        });
                    }

                    break;
                case GenerationOptions.AimTypes.CommunityLearningFM10ValidAims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                        });
                    }

                    fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = LearnDelFAMType.ASL.ToString(),
                        LearnDelFAMCode = ((int)LearnDelFAMCode.ASL_Personal).ToString()
                    });
                    break;
                case GenerationOptions.AimTypes.NonfundedValidAims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                        });
                    }

                    break;
                case GenerationOptions.AimTypes.YP1619Aims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_1619).ToString()
                        });
                    }

                    break;
                case GenerationOptions.AimTypes.AdultFM35ValidAims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                        });
                    }

                    if (Options.LD.IncludeFFI)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Co).ToString()
                        });
                    }

                    break;
                case GenerationOptions.AimTypes.OtherAdultFM81ValidAims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                        });
                    }

                    if (Options.LD.IncludeFFI)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Co).ToString()
                        });
                    }

                    break;
                case GenerationOptions.AimTypes.ESFValidAims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_Adult).ToString()
                        });
                    }

                    if (Options.LD.IncludeFFI)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.FFI.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Co).ToString()
                        });
                    }

                    break;
                case GenerationOptions.AimTypes.OtherYP1619Aims:
                    if (Options.LD.IncludeSOF)
                    {
                        fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                        {
                            LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                            LearnDelFAMCode = ((int)LearnDelFAMCode.SOF_ESFA_1619).ToString()
                        });
                    }

                    break;
                default:
                    throw new NotImplementedException($"Generating LD FAMS has failed for AimDefaultType {Options.AimDefaultType}");
            }

            if (Options.LD.IncludeADL)
            {
                fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.ADL.ToString(),
                    LearnDelFAMCode = "1"
                });
            }

            if (Options.LD.IncludeLDM)
            {
                fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.LDM.ToString(),
                    LearnDelFAMCode = Options.LD.OverrideLDM.HasValue ? Options.LD.OverrideLDM.Value.ToString("D3") : ((int)LearnDelFAMCode.LDM_SteelRedundancy).ToString()
                });
            }

            if (Options.LD.IncludeHHS)
            {
                fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.HHS.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.HHS_NoEmploymentWithChildren).ToString()
                });
            }

            if (Options.LD.IncludeRES)
            {
                fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                    LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
                });
            }

            return fams.ToArray();
        }

        private MessageLearnerLearningDeliveryLearningDeliveryHE[] LDHEs(MessageLearnerLearningDelivery ld)
        {
            List<MessageLearnerLearningDeliveryLearningDeliveryHE> hes = new List<MessageLearnerLearningDeliveryLearningDeliveryHE>(4);
            if (Options.LD.IncludeHEFields)
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

            return hes.ToArray();
        }

        private DateTime AYJuly31()
        {
            return DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY|JUL|31]"));
        }

        private DateTime AYPlus1July31()
        {
            return DateTime.Parse(DCT.TestDataGenerator.Helpers.ValueOrFunction("[AY+1|JUL|31]"));
        }
    }
}