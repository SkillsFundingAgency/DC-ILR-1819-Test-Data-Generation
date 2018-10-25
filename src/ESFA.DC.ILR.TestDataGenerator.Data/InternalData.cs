namespace DCT.TestDataGenerator
{
    public enum FullLevel
    {
        Level2,
        Level3
    }

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
        ACT,
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
        PartnerOrganisation,
        USFC,
        USDC,
        UHEO,
        PLBG
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
        LDM_NonApprenticeshipSeaFishing = 355,
        LDM_OwnEmployeeAprenticeship = 356,
        LDM_GroupTrainingAssociation=129,
        LDM_ApprenticeshipTrainingAgency=130,
        LDM_PrincesTrustTeamProgramme= 331,
        LDM_HESA_GeneratedILRfile=352,
        LDM_CommunityLearningMentalHealthPilot = 340,
        LDM_RoTL = 328,
        LDM_MandationtoSkillsTraining = 318,
        LDM_Pilot = 358,
        LDM_LowWages = 363,
        LDM_ProcuredAdultEducationBudget = 357,
        NSA_Socialcare = 14,
        FLN_FEML = 1,
        POD_FItyPercent = 5,
        HEM_Award = 3,
        WPP_DWP = 1,



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
        VOL,
        INV // Invalid type
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

    public enum CommonComponent
    {
        //        CommonComponent CommonComponentDesc
        NotApplicable = -2,
        Unknown = -1,
        KeySkillsinCommunication = 1,
        KeySkillsinApplicationofNumber = 2,
        KeySkillsinInformationandCommunicationTechnology = 3,
        KeySkillsinWorkingwithOthers = 4,
        KeySkillsinImprovingOwnLearningandPerformance = 5,
        KeySkillsinProblemSolving = 6,
        FunctionalSkillsMathematics = 10,
        FunctionalSkillsEnglish = 11,
        FunctionalSkillsInformationandCommunicationTechnology = 12,
        BritishSignLanguage = 20,
        ProjectExtendedProject = 21,
        GCSEMathematics = 30,
        GCSEEnglish = 31,
        GCSEInformationCommunicationTechnology = 32,
        InternationalGCSEMathematics = 33,
        InternationalGCSEEnglish = 34,
        SteppingStoneEnglish = 35,
        SteppingStoneMaths = 36,
        AdditionalUnitsforMicroBusiness = 40
    }

    public enum WithDrawalReason
    {
        OtherPersonalReasons = 44
    }
}
