using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_08_Tests
    {
        [Fact]
        public void Rulename()
        {
            var func = CreateFundModel();
            func.RuleName().Should().Be("FundModel_08");
        }

        [Fact]
        public void LearnerMutatorCount_ProgTypeCoutnWithoutlevel7Degree()
        {
            var func = CreateFundModel();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(ProgType));
            var ldfc = new List<LearnDelFAMCode>() { LearnDelFAMCode.LDM_NonApprenticeshipSeaFishing, LearnDelFAMCode.LDM_NonApprenticeshipSportingExcellence, LearnDelFAMCode.LDM_NonApprenticeshipTheatre };
            // allow for each progtype but take one off for level 7 (there aren't any).
            var magicNumber = fms.Length - 1;
            // add in a row for each NOT exception type by the "non apprenticeship learning delivery monitoring codes"
            magicNumber += ldfc.Count;
            // add in a row for restarts (negative test for exclusions)
            magicNumber++;
            // add in a row for non-funded (negative test for exclusions)
            magicNumber++;
            lms.Should().HaveCount(magicNumber);
        }

        private FundModel_08 CreateFundModel()
        {
            return new FundModel_08();
        }

    }
}
