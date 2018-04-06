using System;
using System.Collections.Generic;
using System.Linq;

namespace DCT.TestDataGenerator
{
    public enum FullLevel
    {
        Level2,
        Level3
    }

    public class DataCache : ILearnerCreatorDataCache
    {
        private Dictionary<ProgType, ApprenticeshipProgrammeTypeAim> _apprenticeShipAims;

        public DataCache()
        {
            _apprenticeShipAims = new Dictionary<ProgType, ApprenticeshipProgrammeTypeAim>();

            _apprenticeShipAims.Add(
                ProgType.Traineeship,
                new ApprenticeshipProgrammeTypeAim()
                {
                    ProgType = ProgType.Traineeship,
                    FworkCode = 0,
                    PwayCode = 0,
                    LearnAimRef = "60158864"
                });
            _apprenticeShipAims.Add(
                ProgType.AdvancedLevelApprenticeship,
                new ApprenticeshipProgrammeTypeAim()
                {
                    ProgType = ProgType.AdvancedLevelApprenticeship,
                    FworkCode = 420,
                    PwayCode = 1,
                    LearnAimRef = "50104767"
                });
            _apprenticeShipAims.Add(
                ProgType.IntermediateLevelApprenticeship,
                new ApprenticeshipProgrammeTypeAim()
                {
                    ProgType = ProgType.IntermediateLevelApprenticeship,
                    FworkCode = 460,
                    PwayCode = 1,
                    LearnAimRef = "5006521X"
                });
            _apprenticeShipAims.Add(
                ProgType.HigherApprenticeshipLevel4,
                new ApprenticeshipProgrammeTypeAim()
                {
                    ProgType = ProgType.HigherApprenticeshipLevel4,
                    FworkCode = 528,
                    PwayCode = 2,
                    LearnAimRef = "60028427"
                });
            _apprenticeShipAims.Add(
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
                });
            _apprenticeShipAims.Add(
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
                });
            _apprenticeShipAims.Add(
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
                });
            //_apprenticeShipAims.Add(ProgType.HigherApprenticeshipLevel7,
            //    new ApprenticeshipProgrammeTypeAim()
            //    {
            //        ProgType = ProgType.HigherApprenticeshipLevel7,
            //        FworkCode = 612,
            //        PwayCode = 1,
            //        LearnAimRef = "00300225"
            //    });
        }

        public int MaximumLearnRefLength()
        {
            return 12;
        }

        public IEnumerable<ApprenticeshipProgrammeTypeAim> ApprenticeshipAims(ProgType pt)
        {
            return _apprenticeShipAims.Values.Where(s => s.ProgType == pt); ;
        }

        public string ESFContractNumber()
        {
            return "ESF-1234567";
        }

        public IEnumerable<string> GCSEGrades()
        {
            var result = new List<string>
            { "A*", "A", "B", "C", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            };
            result.AddRange(GCSEDOrBelow());
            return result;
        }

        public IEnumerable<string> GCSEDOrBelow()
        {
            return new List<string> { "D", "DD", "DE", "E", "EE", "EF", "F", "FF", "FG", "G", "GG", "N", "U" };
        }

        public LearnAimFunding LearnAimFundingWithValidity(FundModel fm, LearnDelFAMCode sofCode, DateTime learnStartDate)
        {
            switch (fm)
            {
                case FundModel.NonFunded:
                    switch (sofCode)
                    {
                        case LearnDelFAMCode.SOF_HEFCE:
                            return new LearnAimFunding()
                            {
                                LearnAimRef = "40005240",
                                FundModel = fm
                            };
                        default:
                            throw new NotImplementedException($"LearnAimFundingWithValidity asked for FundingModel {fm} but with not implemented Source of Funding (SOF) {sofCode}");
                    }

                case FundModel.Adult:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "60039309",
                        FundModel = fm
                    };
                default:
                    throw new NotImplementedException($"LearnAimFundingWithValidity asked for not implemented FundingModel {fm} also Source of Funding (SOF) {sofCode}");
            }
        }

        public LearnAimFunding LearnAimWithCategory(LearnDelCategory category)
        {
            switch (category)
            {
                case LearnDelCategory.TradeUnion:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "6013463X",
                        FundModel = FundModel.Adult
                    };
                default:
                    throw new NotImplementedException($"LearnAimWithCategory asked for not implemented Category {category}");
            }
        }

        public LearnAimFunding LearnAimWithLearnAimType(LearnAimType aimType)
        {
            switch (aimType)
            {
                case LearnAimType.GCE_A_level:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "50027360",
                        FundModel = FundModel.Adult
                    };
                case LearnAimType.GCE_A2_Level:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "00285814",
                        FundModel = FundModel.Adult
                    };
                case LearnAimType.GCE_Applied_A_Level:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "1004291X",
                        FundModel = FundModel.Adult
                    };
                case LearnAimType.GCE_Applied_A_Level_Double_Award:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "1004260X",
                        FundModel = FundModel.Adult
                    };
                case LearnAimType.GCE_A_Level_with_GCE_Advanced_Subsidiary:
                    return new LearnAimFunding()
                    {
                        LearnAimRef = "50036695",
                        FundModel = FundModel.Adult
                    };
                default:
                    throw new NotImplementedException($"LearnAimWithLearnAimType asked for not implemented type {aimType}");
            }
        }

        public LearnAimFunding LearnAimWithLevel(FullLevel level, FundModel fm)
        {
            switch (fm)
            {
                case FundModel.Adult:
                    {
                        switch (level)
                        {
                            case FullLevel.Level2:
                                return new LearnAimFunding()
                                {
                                    LearnAimRef = "60145286",
                                    FundModel = FundModel.Adult
                                };
                            case FullLevel.Level3:
                                return new LearnAimFunding()
                                {
                                    LearnAimRef = "60061133",
                                    FundModel = FundModel.Adult
                                };
                            default:
                                throw new NotImplementedException($"LearnAimWithLevel asked for not implemented level {level}");
                        }
                    }

                default:
                    throw new NotImplementedException($"LearnAimWithLevel asked for not implemented FundModel {fm} (level) {level}");
            }
        }

        public Organisation OrganisationWithLegalType(LegalOrgType type)
        {
            switch (type)
            {
                case LegalOrgType.SpecialistDesignatedCollege:
                    return new Organisation()
                    {
                        UKPRN = 10001463
                    };
                case LegalOrgType.DummyOrganisationTestingOnly:
                    return new Organisation()
                    {
                        UKPRN = 90000064
                    };
                case LegalOrgType.NotExist:
                    return new Organisation()
                    {
                        UKPRN = 99900064
                    };
                case LegalOrgType.PartnerOrganisation:
                    return new Organisation()
                    {
                        UKPRN = 99999999
                    };
                default:
                    throw new NotImplementedException($"OrganisationWithLegalType asked for not implemented type {type}");
            }
        }

        public IEnumerable<string> InvalidPostcode()
        {
            List<string> invalid0 = new List<string> { "XXX", "x", "xx" };
            List<string> invalid1 = new List<string> { "X1", "333", "X" };
            List<string> invalid2 = new List<string> { "LC", "IL", "LK", "ML", "LO", "VL" };
            List<string> result = new List<string>(50);

            foreach (var i0 in invalid0)
            {
                result.Add(i0 + "2 3AA");
            }

            foreach (var i1 in invalid1)
            {
                result.Add("AA" + i1 + " 3AA");
            }

            foreach (var i2 in invalid2)
            {
                result.Add("AA1A " + i2);
            }

            return result;
        }

        public IEnumerable<LLDDCatValidity> LLDDCatValidity()
        {
            return new List<LLDDCatValidity>()
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
    }
}
