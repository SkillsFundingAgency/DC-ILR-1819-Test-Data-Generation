using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_06_Tests
    {
        [Fact]
        public void Rulename()
        {
            var func = TestHelpers.CreateFunctor<FundModel_06>();
            func.RuleName().Should().Be("FundModel_06");
        }

        [Fact]
        public void LearnerMutatorCount_ProgTypeCoutnWithoutlevel7Degree()
        {
            var func = TestHelpers.CreateFunctor<FundModel_06>();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(ProgType));
            lms.Should().HaveCount(fms.Length-1);
        }
    }
}
