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
            var func = CreateFundModel();
            func.RuleName().Should().Be("FundModel_06");
        }

        [Fact]
        public void LearnerMutatorCount_ProgTypeCoutnWithoutlevel7Degree()
        {
            var func = CreateFundModel();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(ProgType));
            lms.Should().HaveCount(fms.Length-1);
        }

        private FundModel_06 CreateFundModel()
        {
            return new FundModel_06();
        }

    }
}
