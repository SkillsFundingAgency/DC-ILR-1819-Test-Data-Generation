namespace DCT.TestDataGenerator
{
    using System;
    using System.Linq;
    using DCT.ILR.Model;

    public enum ProvSpecLearnMonOccur
    {
        A,
        B,
        C
    }

    public enum LearnAimType
    {
        GCE_A_level = 0002,
        GCE_A2_Level = 1413,
        GCE_Applied_A_Level = 1430,
        GCE_Applied_A_Level_Double_Award = 1431,
        GCE_A_Level_with_GCE_Advanced_Subsidiary = 1453
    }

    public enum LearnerFAMType
    {
        HNS,
        EHC,
        DLA,
        LSR,
        SEN,
        NLM,
        EDF,
        MCF,
        ECF,
        FME,
        PPE
    }

    public enum LearnerFAMCode
    {
        HNS_Yes = 1,
        EHC_Yes = 1,
        EDF_MathsNotGot = 1,
        EDF_EnglishNotGot = 2,
        MCF_ExcemptLearningDifficulty = 1,
        MCF_ExcemptOverseasEquivalent = 2,
        MCF_MetUKEquivalent = 3,
        MCF_MetOtherInstitution = 4,
        MCF_Unassigned1 = 5,
        MCF_Unassigned2 = 6,
        ECF_ExcemptLearningDifficulty = 1,
        ECF_ExcemptOverseasEquivalent = 2,
        ECF_MetUKEquivalent = 3,
        ECF_MetOtherInstitution = 4,
        ECF_Unassigned1 = 5,
        ECF_Unassigned2 = 6,
        DLA_Yes = 1,
        LSR_CareToLearn = 36,
        LSR_Vulnerable = 55,
        LSR_Discretionary = 56,
        LSR_Residential = 57,
        LSR_Hardship = 58,
        LSR_Childcare = 59,
        LSR_ResidentialAccess = 60,
        LSR_ESFWithChildcare = 61,
        LSR_Unassigned1 = 62,
        LSR_Unassigned2 = 63,
        LSR_Unassigned3 = 64,
        LSR_Unassigned4 = 65,
        SEN_Yes = 1,
        NLM_Merger = 17,
        NLM_ContractLevel = 18,
        NLM_Unassigned1 = 21,
        NLM_Unassigned2 = 22,
        NLM_Unassigned3 = 23,
        NLM_Unassigned4 = 24,
        FME_1415Eligible = 1,
        FME_1619Receipt = 2,
        PPE_ServiceChild = 1,
        PPE_AdoptedCare = 2,
        PPE_Unassigned1 = 3,
        PPE_Unassigned2 = 4,
        PPE_Unassigned3 = 5,
    }

    public enum Accomodation
    {
        AwayFromHome = 5
    }

    public enum ContactPrefType
    {
        RUI, // restricted use indicator
        PMC, // preferred method of contact
    }

    public enum ContactPrefCode
    {
        RUI_NoContactCourses = 1,
        RUI_NoContactSurvey = 2,
        RUI_NoContactOldDied = 3,
        RUI_NoContactIllness = 4,
        RUI_NoContactDead = 5,
        PMC_NotPost = 1,
        PMC_NotPhone = 2,
        PMC_NotEmail = 3
    }

    public enum EmploymentStatus
    {
        PaidEmployment = 10,
        LookingForWork = 11,
        NotLookingForWork = 12,
        NoKnown = 98
    }

    public enum EmploymentStatusMonitoringType
    {
        SEI, // Self Employment indicator
        EII, // Employment intestity indicator
        LOU, // length of unemployment
        LOE, // length of employment
        BSI, // Benefit status indicator
        PEI, // previous education indicator
        SEM // small employer
    }

    public enum EmploymentStatusMonitoringCode
    {
        SelfEmployed = 1,
        EmploymentIntensity16Plus = 1,
        EmploymentIntensity16Less = 2,
        EmploymentIntensity1619 = 3,
        EmploymentIntensity20Plus = 4,
        UnemployedLess6 = 1,
        Unemployed611 = 2,
        Unemployed1223 = 3,
        Unemployed2435 = 4,
        Unemployed36 = 5,
        Employed3 = 1,
        Employed46 = 2,
        Employed712 = 3,
        Employed12Plus = 4,
        BenefitJobSeekers = 1,
        BenefitEmploymentSupport = 2,
        BenefitOther = 3,
        BenefitUC = 4,
        EducationPrior = 1,
        SmallEmployer = 1
    }

    public enum PriorAttain
    {
        EntryLevel = 9,
        BelowLevel1 = 7,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        OldLevel4 = 4,
        OldLevel5 = 5,
        Level4 = 10,
        Level5 = 11,
        Level6 = 12,
        Level7 = 13,
        OtherNotKnown = 97,
        NotKnown = 98,
        NoQualifications = 99
    }

    public enum Ethnicity
    {
        WhiteBritish = 31,
        Irish = 32,
        GypsyOrIrishTraveller = 33,
        OtherWhite = 34,
        WhiteAndBlackCaribbean = 35,
        WhiteAndBlackAfrican = 36,
        WhiteAndAsian = 37,
        AnyOtherMixed = 38,
        Indian = 39,
        Pakistani = 40,
        Bangladeshi = 41,
        Chinese = 42,
        AnyOtherAsian = 43,
        African = 44,
        Caribbean = 45,
        AnyOtherBlack = 46,
        Arab = 47,
        AnyOther = 98,
        NotProvided = 99
    }

    public enum Sex
    {
        M,
        F
    }

    public enum LLDDCat
    {
        Emotional = 1,
        MultipleDisabilities = 2,
        MultipleLearningDisabilities = 3,
        VisualImpairment = 4,
        HearingImpairement = 5,
        Mobility = 6,
        ProfoundComplex = 7,
        SocialAndEmotional = 8,
        MentalHealth = 9,
        ModerateLearning = 10,
        SevereLearning = 11,
        Dyslexia = 12,
        Dyscalculia = 13,
        Autism = 14,
        Aspergers = 15,
        Temporary = 16,
        SpeechLanguage = 17,
        OtherPhysical = 93,
        OtherSpecific = 94,
        OtherMedical = 95,
        OtherLearningDifficulty = 96,
        OtherDisability = 97,
        PreferNotToSay = 98,
        NotProvided = 99
    }

    public enum LLDDHealthProb
    {
        LearningDifficultyOrHealthProblem = 1,
        NoLearningDifficultOrHealthProblem = 2,
        NotProvided = 9
    }

    public enum AimType
    {
        ProgrammeAim = 1,
        ComponentAim = 3,
        StandAlone = 4,
        CoreAim1619 = 5
    }

    public enum ProgType
    {
        AdvancedLevelApprenticeship = 2,
        IntermediateLevelApprenticeship = 3,
        HigherApprenticeshipLevel4 = 20,
        HigherApprenticeshipLevel5 = 21,
        HigherApprenticeshipLevel6 = 22,
        HigherApprenticeshipLevel7 = 23,
        Traineeship = 24,
        ApprenticeshipStandard = 25
    }

    public enum CompStatus
    {
        Continuing = 1,
        Completed = 2,
        Withdrawn = 3,
        BreakInLearning = 6
    }

    public enum LearnDelAppFinType
    {
        TNP,
        PMR
    }

    public enum LearnDelAppFinCode
    {
        TotalTrainingPrice = 1,
        TotalAssessmentPrice = 2,
        ResidualTrainingPrice = 3,
        ResidualAssessmentPrice = 4,
        TrainingPayment = 1,
        AssessmentPayment = 2,
        EmployerReimbursement = 3
    }

    public enum LearnDelFAMType
    {
        SOF,
        FFI,
        EEF,
        RES,
        LSF,
        ADL,
        ALB,
        ASL,
        FLN,
        LDM,
        NSA,
        WPP,
        POD,
        HEM,
        HHS,
        ACT
    }

    public enum LearnDelCategory
    {
        TradeUnion = 19
    }

    public enum LegalOrgType
    {
        SpecialistDesignatedCollege,
        DummyOrganisationTestingOnly,
        NotExist,
        PartnerOrganisation
    }

    public enum LearnDelFAMCode
    {
        SOF_HEFCE = 1,
        SOF_ESFA_Adult = 105,
        SOF_ESFA_1619 = 107,
        SOF_LA = 108,
        SOF_Other = 998,
        SOF_Unassigned = 110,
        FFI_Fully = 1,
        FFI_Co = 2,
        EEF_Apprenticeship_19 = 2,
        EEF_Apprenticeship_24 = 3,
        EEF_Apprenticeship_Extended = 4,
        RES = 1,
        LSF = 1,
        ADL = 1,
        ALB_Rate_1 = 1,
        ALB_Rate_2 = 2,
        ALB_Rate_3 = 3,
        ASL_Personal = 1,
        ASL_Neighbour = 2,
        ASL_FamilyEnglishMathsLanguage = 3,
        ASL_WiderFamily = 4,
        ACT_ContractEmployer = 1,
        ACT_ContractESFA = 2,
        HHS_NoEmploymentWithChildren = 1,
        HHS_NoEmploymentNoChildren = 2,
        HHS_SingleWithChildren = 3,
        HHS_Withheld,
        HHS_NoneAbove,
        LDM_OLASS = 034,
        LDM_Military = 346,
        LDM_SteelRedundancy = 347,
        LDM_SolentCity = 339,
        LDM_NonApprenticeshipSportingExcellence = 353,
        LDM_NonApprenticeshipTheatre = 354,
        LDM_NonApprenticeshipSeaFishing = 355

        // Add more here
    }

    public enum FundModel
    {
        CommunityLearning = 10,
        YP1619 = 25,
        Adult = 35,
        Apprenticeships = 36,
        ESF = 70,
        OtherAdult = 81,
        OtherYP1619 = 82,
        NonFunded = 99
    }

    public enum QualificationOnEntry
    {
        Q51, // 14-19 Higher Diploma (Level 2)
        Q52, // Welsh Baccalaureate Intermediate Diploma (Level 2)
        Q80, // Other Qualification at Level 2
        R51, // 14-19 Foundation Diploma (Level 1)
        R52, // Welsh Baccalaureate Foundation Diploma (Level 1)
        R80, // Other Qualification at Level 1
        X00, // HE Access Course, QAA recognised
        X01, // HE Access Course, not QAA recognised
        X02, // Mature student admitted on basis of previous experience and/or admissions test
        X03, // Mature students admitted on basis of previous experience (without formal
        APELAPL, // and/or institution's own entrance examinations) 31/07/2013
        X04, // Other qualification level not known
        X05, // Student has no formal qualification
        X06 // Not known
    }

    public enum TypeOfyear
    {
        FEYear = 1,
        NotFEYear = 2,
        CommencingAcrossYears = 3,
        MidwayAcrossYears = 4,
        FinisedAcrossYears = 5
    }

    public enum ModeOfStudy
    {
        FullTime = 1,
        SandwichYear = 2,
        PartTime = 3,
        NotInPopulation = 99
    }

    public enum FundingLevel
    {
        Undergraduate = 10,
        LongUndergraduate = 11,
        PostgraduateTaught = 20,
        LongPostgraduateTaught = 21,
        PostgraduateResearch = 30,
        LongPostgraduateResearch = 31,
        NotinHEIFESpopulation = 99
    }

    public enum FundingCompletion
    {
        Completed = 1,
        NotCompleted = 2,
        NotYetCompleted = 3,
        NotinHEIFESpopulation = 9
    }

    public enum MajorSourceOfTuitionFees
    {
        NoAward = 1
            /* plenty missing */
    }

    public enum SpecialFeeIndicator
    {
        Standard = 0,
        SandwichPlacement = 1,
        LanguageYearAbroad = 2,
        FullOutgoingERASMUS = 3,
        FinalYearfulltimeLess15Weeks = 4,
        FinalYearfulltimeMore14Weeks = 5,
        Other = 9
    }

    public enum EquivalentLowerQualification
    {
        NonExempt = 1,
        Exempt = 2,
        NotELQ = 3,
        NotRequired = 9
    }

    public enum Outcome
    {
        Achieved = 1,
        Partial = 2,
        NoAchievement = 3,
        NotYetKnown = 8
    }

    public enum OutcomeType
    {
        EDU,
        EMP,
        GAP,
        NPE,
        OTH,
        SDE,
        VOL
    }

    public enum OutcomeCode
    {
        EDU_Traineeship = 1,
        EDU_Apprenticeship = 2,
        EDU_Internship = 3,
        EDU_FullTime_FE = 4,
        EDU_PartTime_FE = 5,
        EMP_PaidMore16 = 1,
        EMP_PaidLess16 = 2,
        EMP_Self = 3,
        EMP_SelfMore16 = 4,
        EMP_SelfLess16 = 5,
        VOL_Work = 1

        // others go here
    }

    public class Helpers
    {
        /// <summary>
        /// Achievement dates are an apprenticeship (or traineeship) concept and will fail other rules if set when they shouldn't be
        /// </summary>
        public enum SetAchDate
        {
            /// <summary>
            /// Traineeships and FM36 apprenticeships should set the AchDate
            /// </summary>
            SetAchDate,
            DoNotSetAchDate
        }

        public enum AgeRequired
        {
            Exact1Day,
            Exact4,
            Exact13,
            Exact15,
            Less16,
            Less16And30Days,
            Exact16,
            Less18,
            Exact18,
            Less19,
            Exact19,
            Less24,
            Exact24,
            More24,
            Less25,
            Exact25,
            Less100,
            Less115
        }

        public enum BasedOn
        {
            /// <summary>
            /// The normal FE Accademic year starts on 01-August
            /// </summary>
            AYStart,

            /// <summary>
            /// The FE AY ends on the 31-July
            /// </summary>
            EndAYYear,

            /// <summary>
            /// The school AY is based on the end of August
            /// </summary>
            SchoolAYStart,

            /// <summary>
            /// Dates in rules are often not based on the year but on the dates of learning delivery
            /// </summary>
            LearnDelStart
        }

        public enum MakeOlderOrYoungerWhenInvalid
        {
            Older,
            OlderTwo,
            YoungerLots,
            Younger,
            NoChange
        }

        public static string ValueOrFunction(string val)
        {
            if (val.StartsWith("["))
            {
                if (val.EndsWith("]"))
                {
                    val = val.Substring(1, val.Length - 2);
                }

                var command = val.Split('|');
                switch (command[0])
                {
                    case "Age":
                        int ageInYears = int.Parse(command[1]);
                        DateTime now = DateTime.Now;
                        now -= TimeSpan.FromDays(365 * ageInYears);
                        return now.ToString("yyyy-MM-dd");
                    case "AY":
                        // "AY|JUN|06"
                        int month = ConvertToMonth(command[1]);
                        int year = DateTime.Now.Year;
                        if (month >= 8)
                        {
                            --year;
                        }

                        DateTime d = new DateTime(year, month, int.Parse(command[2]));
                        return d.ToString("yyyy-MM-dd");
                    case "AY+1":
                        // "AY|JUN|06"
                        int month1 = ConvertToMonth(command[1]);
                        int year1 = DateTime.Now.Year;
                        ++year1;
                        if (month1 >= 8)
                        {
                            --year1;
                        }

                        DateTime d1 = new DateTime(year1, month1, int.Parse(command[2]));
                        return d1.ToString("yyyy-MM-dd");
                    case "AY-1":
                        // "AY|JUN|06"
                        int month2 = ConvertToMonth(command[1]);
                        int year2 = DateTime.Now.Year;
                        --year2;
                        if (month2 >= 8)
                        {
                            --year2;
                        }

                        DateTime d2 = new DateTime(year2, month2, int.Parse(command[2]));
                        return d2.ToString("yyyy-MM-dd");
                    case "GEN":
                    case "OPT":
                    case "VALID4PROP":
                        return val;
                    case "EMPTY":
                        return null;
                    case "FMTOAIM":
                        return val;
                    case "DEFAULT":
                        return val;

                    default:
                        throw new ArgumentException($"ValueOrFunction detected func {command[0]} extracted from {val} but there is no command handler for that");
                }
            }
            else
            {
                return val;
            }
        }

        public static void MutateLearningDeliveryMonitoringLDMToNewCode(MessageLearner learner, LearnDelFAMCode code)
        {
            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.LDM.ToString()).First();
            fam.LearnDelFAMCode = ((int)code).ToString("D3");
        }

        /// <summary>
        /// Learner should have been created with LearnerTypeRequired.Apprenticeships to ensure that the programme and app fin records and similar have been created properly
        /// </summary>
        /// <param name="learner">Learner to mutate to FM35 from FM36</param>
        public static void MutateApprenticeshipToOlderFullyFunded(MessageLearner learner)
        {
            learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate;
            learner.LearningDelivery[0].FundModel = (int)FundModel.Adult;
            learner.LearningDelivery[1].FundModel = (int)FundModel.Adult;
            MoveEmploymentBeforeLearnStart(learner);
            learner.LearningDelivery[0].AppFinRecord = null;

            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ACT.ToString()).First();
            fam.LearnDelFAMType = LearnDelFAMType.FFI.ToString();
            fam.LearnDelFAMDateFromSpecified = false;
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString();

            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = fam.LearnDelFAMType,
                LearnDelFAMCode = fam.LearnDelFAMCode
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
        }

        /// <summary>
        /// Learner should have been created with LearnerTypeRequired.Apprenticeships to ensure that the programme and app fin records and similar have been created properly
        /// </summary>
        /// <param name="learner">Learner to mutate to a standards based apprenticeship</param>
        public static void MutateApprenticeshipToStandard(MessageLearner learner)
        {
            learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate;
            learner.LearningDelivery[0].FundModel = (int)FundModel.OtherAdult;
            learner.LearningDelivery[1].FundModel = (int)FundModel.OtherAdult;
            MoveEmploymentBeforeLearnStart(learner);
            learner.LearningDelivery[0].AppFinRecord[0].AFinDate = learner.LearningDelivery[0].LearnStartDate;
            var appfins = learner.LearningDelivery[0].AppFinRecord.ToList();
            appfins.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = 500,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.TNP.ToString(),
                AFinCode = (int)LearnDelAppFinCode.TotalAssessmentPrice,
                AFinCodeSpecified = true,
                AFinDate = appfins[0].AFinDate,
                AFinDateSpecified = true
            });

            learner.LearningDelivery[0].AppFinRecord = appfins.ToArray();
            learner.LearningDelivery[0].EPAOrgID = "EPA0032";

            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ACT.ToString()).First();
            fam.LearnDelFAMType = LearnDelFAMType.FFI.ToString();
            fam.LearnDelFAMDateFromSpecified = false;
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString();

            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = fam.LearnDelFAMType,
                LearnDelFAMCode = fam.LearnDelFAMCode
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
        }

        public static void AddRestartFAMToLearningDelivery(MessageLearner learner)
        {
            var ld0Fams = learner.LearningDelivery[0].LearningDeliveryFAM.ToList();
            ld0Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
            });
            learner.LearningDelivery[0].LearningDeliveryFAM = ld0Fams.ToArray();
        }

        public static void SetEndDates(MessageLearnerLearningDelivery ld, DateTime endDate, SetAchDate modifyAch)
        {
            ld.LearnPlanEndDate = endDate;
            ld.LearnActEndDate = endDate;
            ld.LearnActEndDateSpecified = true;
            ld.CompStatus = (int)CompStatus.Completed;
            ld.Outcome = (int)Outcome.Achieved;
            ld.OutcomeSpecified = true;
            if (modifyAch == SetAchDate.SetAchDate)
            {
                ld.AchDate = ld.LearnActEndDate;
                ld.AchDateSpecified = true;
            }
            else
            {
                ld.AchDateSpecified = false;
            }
        }

        public static void MutateApprenticeLearningDeliveryToTrainee(MessageLearner learner, ILearnerCreatorDataCache dataCache)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.ProgTypeSpecified = true;
                ld.FworkCodeSpecified = false;
                ld.PwayCodeSpecified = false;
                ld.FundModel = (int)FundModel.Adult;
                ld.FundModel = (int)FundModel.Adult;
                ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(60);
            }

            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ACT.ToString()).First();
            fam.LearnDelFAMType = LearnDelFAMType.FFI.ToString();
            fam.LearnDelFAMDateFromSpecified = false;
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Co).ToString();

            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = fam.LearnDelFAMType,
                LearnDelFAMCode = fam.LearnDelFAMCode
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
            ApprenticeshipProgrammeTypeAim pta = dataCache.ApprenticeshipAims(ProgType.Traineeship).First();

            learner.LearningDelivery[1].LearnAimRef = pta.LearnAimRef;

            MoveEmploymentBeforeLearnStart(learner);
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring[0].ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity16Less;
            learner.LearningDelivery[0].AppFinRecord = null;
        }

        public static void MoveEmploymentBeforeLearnStart(MessageLearner learner)
        {
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate;
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearnerEmploymentStatus[0].DateEmpStatApp.AddDays(-40);
        }

        public static void MutateDOB(MessageLearner learner, bool valid, AgeRequired ar, BasedOn whatTypeOfAge, MakeOlderOrYoungerWhenInvalid direction)
        {
            switch (whatTypeOfAge)
            {
                case BasedOn.SchoolAYStart:
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|31]"));
                    break;
                case BasedOn.AYStart:
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|01]"));
                    break;
                case BasedOn.EndAYYear:
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[AY|JUL|31]"));
                    break;
                case BasedOn.LearnDelStart:
                    learner.DateOfBirth = learner.LearningDelivery.Min(s => s.LearnStartDate);
                    break;
                default:
                    throw new NotImplementedException($"MutateDOB base date to compute age {whatTypeOfAge} has not been implementated");
            }

            switch (ar)
            {
                case AgeRequired.Exact1Day:
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(-1);
                    break;
                case AgeRequired.Exact4:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-4);
                    break;
                case AgeRequired.Exact13:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-13);
                    break;
                case AgeRequired.Exact15:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-15);
                    break;
                case AgeRequired.Exact16:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-16);
                    break;
                case AgeRequired.Exact18:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-18);
                    break;
                case AgeRequired.Exact19:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-19);
                    break;
                case AgeRequired.Exact24:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-24);
                    break;
                case AgeRequired.Exact25:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-25);
                    break;
                case AgeRequired.Less16And30Days:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-16);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(-30);
                    break;
                case AgeRequired.Less19:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-19);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less24:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-24);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less25:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-25);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less100:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-100);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less115:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-115);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.More24:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-24);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(-1);
                    break;
                default:
                    throw new NotImplementedException($"MutateDOB exact type of age required {ar} has not been implementated");
            }

            if (!valid)
            {
                switch (direction)
                {
                    case MakeOlderOrYoungerWhenInvalid.Older:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(-1);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.OlderTwo:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(-2);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.Younger:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.YoungerLots:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(63);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.NoChange:
                        break;
                }
            }
        }

        internal static void AddOrChangeSourceOfFunding(MessageLearnerLearningDelivery ld, LearnDelFAMCode sof)
        {
            var ifam = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.SOF.ToString());
            if (ifam.Count() > 0)
            {
                ifam.First().LearnDelFAMCode = ((int)sof).ToString();
            }
            else
            {
                var ld1Fams = ld.LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)sof).ToString()
                });
                ld.LearningDeliveryFAM = ld1Fams.ToArray();
            }
        }

        internal static void SetApprenticeshipAims(MessageLearner learner, ApprenticeshipProgrammeTypeAim pta)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)pta.ProgType;
                if (ld.ProgType == (int)ProgType.ApprenticeshipStandard)
                {
                    ld.StdCode = pta.StdCode;
                    ld.StdCodeSpecified = true;
                    ld.FworkCodeSpecified = false;
                    ld.PwayCodeSpecified = false;
                }
                else
                {
                    ld.FworkCode = pta.FworkCode;
                    ld.PwayCode = pta.PwayCode;
                }
            }

            learner.LearningDelivery[1].LearnAimRef = pta.LearnAimRef;
        }

        internal static void RemoveFFIFromLearningDelivery(MessageLearner learner)
        {
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                var ld0Fams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.FFI.ToString());
                ld.LearningDeliveryFAM = ld0Fams.ToArray();
            }
        }

        internal static void RemoveLearnerFAM(MessageLearner learner, LearnerFAMType type)
        {
            var ifam = learner.LearnerFAM.Where(s => s.LearnFAMType != type.ToString());
            learner.LearnerFAM = ifam.ToArray();
        }

        internal static void AddOrChangeLearnerFAM(MessageLearner learner, LearnerFAMType type, LearnerFAMCode code)
        {
            var ifam = learner.LearnerFAM.Where(s => s.LearnFAMType != type.ToString()).ToList();
            ifam.Add(new MessageLearnerLearnerFAM()
            {
                LearnFAMType = type.ToString(),
                LearnFAMCode = (int)code,
                LearnFAMCodeSpecified = true
            });
            learner.LearnerFAM = ifam.ToArray();
        }

        internal static void AddLearnerFAM(MessageLearner learner, LearnerFAMType type, LearnerFAMCode code)
        {
            var ifam = learner.LearnerFAM.ToList();
            ifam.Add(new MessageLearnerLearnerFAM()
            {
                LearnFAMType = type.ToString(),
                LearnFAMCode = (int)code,
                LearnFAMCodeSpecified = true
            });
            learner.LearnerFAM = ifam.ToArray();
        }

        internal static void AddOrChangeProviderSpecLearnerMonitoring(MessageLearner learner, ProvSpecLearnMonOccur type)
        {
            var ifam = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur != type.ToString()).ToList();
            ifam.Add(new MessageLearnerProviderSpecLearnerMonitoring()
            {
                ProvSpecLearnMon = $"{learner.ULN}",
                ProvSpecLearnMonOccur = type.ToString()
            });
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
        }

        internal static void AddProviderSpecLearnerMonitoring(MessageLearner learner, ProvSpecLearnMonOccur type)
        {
            var ifam = learner.ProviderSpecLearnerMonitoring.ToList();
            ifam.Add(new MessageLearnerProviderSpecLearnerMonitoring()
            {
                ProvSpecLearnMon = $"{learner.ULN}",
                ProvSpecLearnMonOccur = type.ToString()
            });
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
        }

        private static int ConvertToMonth(string v)
        {
            switch (v)
            {
                case "JAN": return 1;
                case "FEB": return 2;
                case "MAR": return 3;
                case "APR": return 4;
                case "MAY": return 5;
                case "JUN": return 6;
                case "JUL": return 7;
                case "AUG": return 8;
                case "SEP": return 9;
                case "OCT": return 10;
                case "NOV": return 11;
                case "DEC": return 12;
                default:
                    throw new ArgumentException($"ConvertToMonth detected month {v} but doesn't understand how to convert to a numerical month value");
            }
        }
    }
}
