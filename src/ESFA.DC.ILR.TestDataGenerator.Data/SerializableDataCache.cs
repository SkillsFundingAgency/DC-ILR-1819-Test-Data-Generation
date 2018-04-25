using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ESFA.DC.Serialization;
using ESFA.DC.Serialization.Interfaces;

namespace DCT.TestDataGenerator
{
    public class SerializableDataCache
    {
        const int _MaximumLearnRefLength = 12;
        const string _ESFContractNumber = "ESF-1234567";

        public Dictionary<ProgType, ApprenticeshipProgrammeTypeAim> _apprenticeShipAims;
        public List<string> _gcseGrades;
        public List<string> _gcseDGrades;
        public List<LLDDCatValidity> _llddCatValidity;
        public List<LearnAimFundingModelSourceOfFunding> _learnAimFundingModels;
        public List<LearnAimFundingModelFullLevel> _learnAimFundingModelFullLevel;
        public Dictionary<LearnDelCategory, LearnAimFunding> _learnAimWithCategory;
        public Dictionary<LearnAimType, LearnAimFunding> _learnAimWithLearnAimType;
        public Dictionary<LegalOrgType, Organisation> _organisations;

        public void CreateFromStaticData()
        {
            PopulateApprenticeshipAims();
            PopulateGCSEGrades();
            PopulateLLDDCatValidity();
            PopulateLearnAimFundingModels();
            PopulateLearnAimWithCategory();
            PopulateLearnAimWithLearnAimType();
            PopulateLearnAimFundingModelFulls();
            PopulateOrganisations();
        }

        public void WriteToFile(string path)
        {
            ISerializationService serializationService = new ESFA.DC.Serialization.Json.JsonSerializationService();
            var content = serializationService.Serialize<SerializableDataCache>(this);
            File.WriteAllText(path, content);
        }

        public void ReadFromFile(string path)
        {
            ISerializationService serializationService = new ESFA.DC.Serialization.Json.JsonSerializationService();
            SerializableDataCache rhs = serializationService.Deserialize<SerializableDataCache>(File.ReadAllText(path));
            this._apprenticeShipAims = rhs._apprenticeShipAims;
            this._gcseDGrades = rhs._gcseDGrades;
            this._gcseGrades = rhs._gcseGrades;
            this._learnAimFundingModelFullLevel = rhs._learnAimFundingModelFullLevel;
            this._learnAimFundingModels = rhs._learnAimFundingModels;
            this._learnAimWithCategory = rhs._learnAimWithCategory;
            this._learnAimWithLearnAimType = rhs._learnAimWithLearnAimType;
            this._llddCatValidity = rhs._llddCatValidity;
            this._organisations = rhs._organisations;
        }

        private void PopulateApprenticeshipAims()
        {
            _apprenticeShipAims = new Dictionary<ProgType, ApprenticeshipProgrammeTypeAim>
            {
                {
                    ProgType.Traineeship,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.Traineeship,
                        FworkCode = 0,
                        PwayCode = 0,
                        LearnAimRef = "60061649"
                    }
                },
                {
                    ProgType.AdvancedLevelApprenticeship,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.AdvancedLevelApprenticeship,
                        FworkCode = 420,
                        PwayCode = 1,
                        LearnAimRef = "60009044" // "50104767"
                    }
                },
                {
                    ProgType.IntermediateLevelApprenticeship,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.IntermediateLevelApprenticeship,
                        FworkCode = 460,
                        PwayCode = 1,
                        LearnAimRef = "5006521X"
                    }
                },
                {
                    ProgType.HigherApprenticeshipLevel4,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.HigherApprenticeshipLevel4,
                        FworkCode = 528,
                        PwayCode = 2,
                        LearnAimRef = "60028427"
                    }
                },
                {
                    ProgType.HigherApprenticeshipLevel5,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.HigherApprenticeshipLevel5,
                        FworkCode = 584,
                        PwayCode = 2,
                        LearnAimRef = "00300728",
                        Validity = new List<Validity>()
                    {
                        new Validity()
                        {
                            From = DateTime.Parse("2014-SEP-22")
                        }
                    }
                    }
                },
                {
                    ProgType.HigherApprenticeshipLevel6,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.HigherApprenticeshipLevel6,
                        FworkCode = 612, //615
                        PwayCode = 2,
                        LearnAimRef = "00300225", // 00300355
                        Validity = new List<Validity>()
                    {
                        new Validity()
                        {
                            From = DateTime.Parse("2013-OCT-07")
                        }
                    }
                    }
                },
                {
                    ProgType.ApprenticeshipStandard,
                    new ApprenticeshipProgrammeTypeAim()
                    {
                        ProgType = ProgType.ApprenticeshipStandard,
                        StdCode = 1,
                        PwayCode = 0,
                        LearnAimRef = "50098184", // 00300355
                        Validity = new List<Validity>()
                    {
                        new Validity()
                        {
                            From = DateTime.Parse("2013-MAR-18")
                        }
                    }
                    }
                }
            };
        }

        private void PopulateGCSEGrades()
        {
            _gcseDGrades = new List<string> { "D", "DD", "DE", "E", "EE", "EF", "F", "FF", "FG", "G", "GG", "N", "U" };
            _gcseGrades = new List<string>
            { "A*", "A", "B", "C", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            };
            _gcseGrades.AddRange(_gcseDGrades);
        }

        private void PopulateLLDDCatValidity()
        {
            _llddCatValidity = new List<LLDDCatValidity>()
            {
                new TestDataGenerator.LLDDCatValidity()
                {
                    Category = LLDDCat.Emotional,
                    To = DateTime.Parse("2015-JUL-31")
                },
                new TestDataGenerator.LLDDCatValidity()
                {
                    Category = LLDDCat.MultipleDisabilities,
                    To = DateTime.Parse("2015-JUL-31")
                },
                new TestDataGenerator.LLDDCatValidity()
                {
                    Category = LLDDCat.MultipleLearningDisabilities,
                    To = DateTime.Parse("2015-JUL-31")
                }
            };
        }

        private void PopulateLearnAimFundingModelFulls()
        {
            _learnAimFundingModelFullLevel = new List<LearnAimFundingModelFullLevel>()
            {
                new LearnAimFundingModelFullLevel()
                {
                    Level = FullLevel.Level2,
                    AimFunding = new LearnAimFunding()
                    {
                        LearnAimRef = "60145286",
                        FundModel = FundModel.Adult
                    }
                },
                new LearnAimFundingModelFullLevel()
                {
                    Level = FullLevel.Level3,
                    AimFunding = new LearnAimFunding()
                    {
                        LearnAimRef = "60061133",
                        FundModel = FundModel.Adult
                    }
                }
            };
        }

        private void PopulateLearnAimFundingModels()
        {
            _learnAimFundingModels = new List<LearnAimFundingModelSourceOfFunding>()
            {
                new LearnAimFundingModelSourceOfFunding()
                {
                    SourceOfFunding = LearnDelFAMCode.SOF_HEFCE,
                    AimFunding = new LearnAimFunding()
                    {
                        LearnAimRef = "40005240",
                        FundModel = FundModel.NonFunded
                    }
                },
                new LearnAimFundingModelSourceOfFunding()
                {
                    SourceOfFunding = LearnDelFAMCode.SOF_ESFA_Adult,
                    AimFunding = new LearnAimFunding()
                    {
                        LearnAimRef = "60039309",
                        FundModel = FundModel.Adult
                    }
                }
            };
        }

        private void PopulateLearnAimWithCategory()
        {
            _learnAimWithCategory = new Dictionary<LearnDelCategory, LearnAimFunding>()
            {
                {
                    LearnDelCategory.TradeUnion,
                    new LearnAimFunding()
                    {
                        LearnAimRef = "6013463X",
                        FundModel = FundModel.Adult
                    }
                }
            };
        }

        private void PopulateLearnAimWithLearnAimType()
        {
            _learnAimWithLearnAimType = new Dictionary<LearnAimType, LearnAimFunding>()
            {
                { LearnAimType.GCE_A_level,
                    new LearnAimFunding()
                    {
                        LearnAimRef = "50027360",
                        FundModel = FundModel.Adult
                    }
                },
                {
                LearnAimType.GCE_A2_Level,
                    new LearnAimFunding()
                    {
                        LearnAimRef = "00285814",
                        FundModel = FundModel.Adult
                    }
                },
                {
                    LearnAimType.GCE_Applied_A_Level,
                    new LearnAimFunding()
                    {
                        LearnAimRef = "1004291X",
                        FundModel = FundModel.Adult
                    }
                },
                {
                    LearnAimType.GCE_Applied_A_Level_Double_Award,
                    new LearnAimFunding()
                    {
                        LearnAimRef = "1004260X",
                        FundModel = FundModel.Adult
                    }
                },
                {
                    LearnAimType.GCE_A_Level_with_GCE_Advanced_Subsidiary,
                    new LearnAimFunding()
                    {
                        LearnAimRef = "50036695",
                        FundModel = FundModel.Adult
                    }
                }
            };
        }

        private void PopulateOrganisations()
        {
            _organisations = new Dictionary<LegalOrgType, Organisation>()
            {
                {
                    LegalOrgType.SpecialistDesignatedCollege,
                    new Organisation()
                    {
                        UKPRN = 10001463
                    }
                },
                {
                    LegalOrgType.DummyOrganisationTestingOnly,
                    new Organisation()
                    {
                        UKPRN = 90000064
                    }
                },
                {
                    LegalOrgType.NotExist,
                    new Organisation()
                    {
                        UKPRN = 99900064
                    }
                },
                {
                    LegalOrgType.PartnerOrganisation,
                    new Organisation()
                    {
                        UKPRN = 99999999
                    }
                }
            };
        }

    }
}
