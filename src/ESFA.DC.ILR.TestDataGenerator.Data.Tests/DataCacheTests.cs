using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator
{
    public class DataCacheTests
    {
        [Fact]
        public void ApprenticeshipAims_ExistForAllProgrammesExceptHigher()
        {
            var programmes = Enum.GetValues(typeof(ProgType));
            var cache = CreatorDataCache();
            foreach (var programme in programmes)
            {
                var prog = (ProgType)programme; //  HigherApprenticeshipLevel7 = 23,

                IEnumerable< ApprenticeshipProgrammeTypeAim> aims = cache.ApprenticeshipAims((ProgType)programme);
                int count = LookupProgTypeMinimumNumberOfAims(prog);
                aims.Should().HaveCountGreaterOrEqualTo( count,"Each programme {0} should have at least {1} aim available to it for testing",prog,count);
            }
        }

        [Fact]
        public void ESFContractNumber_NotNullOrEmpty()
        {
            var cache = CreatorDataCache();
            cache.ESFContractNumber().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GCSEGrades_IncludesGradeDandBelow()
        {
            var cache = CreatorDataCache();
            IEnumerable<string> grades = cache.GCSEGrades();
            IEnumerable<string> dGrades = cache.GCSEDOrBelow();

            dGrades.Should().HaveCountLessThan(grades.Count());
            grades.Should().Contain(dGrades);
        }

        [Fact]
        public void LearnAimFundingWithValidity_NonFundedHEFCE_NotEmpty()
        {
            var cache = CreatorDataCache();
            cache.LearnAimFundingWithValidity(FundModel.NonFunded, LearnDelFAMCode.SOF_HEFCE, DateTime.Now).Should().NotBeNull();
        }

        [Fact]
        public void LearnAimFundingWithValidity_Adult_NotEmpty()
        {
            var cache = CreatorDataCache();
            cache.LearnAimFundingWithValidity(FundModel.Adult, LearnDelFAMCode.SOF_ESFA_Adult, DateTime.Now).Should().NotBeNull();
        }

        [Fact]
        public void LearnAimWithCategory_TradeUnion_NotNull()
        {
            var cache = CreatorDataCache();
            cache.LearnAimWithCategory(LearnDelCategory.TradeUnion).Should().NotBeNull();
        }

        [Fact]
        public void LearnAimWithLearnAimType_AllLearnAimTypes_NotNull()
        {
            var cache = CreatorDataCache();
            var lats = Enum.GetValues(typeof(LearnAimType));
            foreach (var lat in lats)
            {
                var t = (LearnAimType)lat;
                cache.LearnAimWithLearnAimType(t).Should().NotBeNull();
            }
        }

        [Fact]
        public void LearnAimWithLevel_AllLevelsAdultFunded_NotNull()
        {
            var cache = CreatorDataCache();
            var levels = Enum.GetValues(typeof(FullLevel));
            foreach (var level in levels)
            {
                var l = (FullLevel)level;
                cache.LearnAimWithLevel(l,FundModel.Adult).Should().NotBeNull();
            }
        }

        [Fact]
        public void Organisation_AllLegalTypes_NotNull()
        {
            var cache = CreatorDataCache();
            var types = Enum.GetValues(typeof(LegalOrgType));
            foreach (var type in types)
            {
                var t = (LegalOrgType)type;
                cache.OrganisationWithLegalType(t).Should().NotBeNull();
            }
        }

        private int LookupProgTypeMinimumNumberOfAims(ProgType prog)
        {
            switch(prog)
            {
                case ProgType.HigherApprenticeshipLevel7:
                    return 0;
                default:
                    return 1;
            }
        }

        [Fact]
        public void InvalidPostcode_Combinations_CountGreaterThan12()
        {
            var cache = CreatorDataCache();
            cache.InvalidPostcode().Should().HaveCountGreaterOrEqualTo(12);
        }

        [Fact]
        public void LLDDCatValidity_CountThree()
        {
            var cache = CreatorDataCache();
            cache.LLDDCatValidity().Should().HaveCount(3);
        }

        [Fact]
        public void LearningDelivery_SteppingStoneEnglish_NotNull()
        {
            var cache = CreatorDataCache();
            LearningDelivery ld = cache.LearningDeliveryWithCommonComponent(CommonComponent.SteppingStoneEnglish);
            ld.Should().NotBeNull();
        }

        [Fact]
        public void LearningDelivery_NotSteppingStoneEnglish_InvalidOperationException()
        {
            var cache = CreatorDataCache();
            var types = Enum.GetValues(typeof(CommonComponent));
            foreach (var type in types)
            {
                var t = (CommonComponent)type;
                if (t != CommonComponent.SteppingStoneEnglish)
                {
                    Action call = () => { cache.LearningDeliveryWithCommonComponent(t); };
                    call.Should().Throw<InvalidOperationException>();
                }
            }
        }

        private ILearnerCreatorDataCache CreatorDataCache()
        {
            return new DataCache();
        }
    }
}
