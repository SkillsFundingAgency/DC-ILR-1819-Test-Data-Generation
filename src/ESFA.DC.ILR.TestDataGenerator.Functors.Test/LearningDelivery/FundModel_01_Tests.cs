using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_01_Tests
    {
        [Fact]
        public void LearnerCount_FundModelCount()
        {
            var func = CreateFundModel();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(FundModel));
            lms.Should().HaveCount(fms.Length);
        }

        [Fact]
        public void Rulename()
        {
            var func = CreateFundModel();
            func.RuleName().Should().Be("FundModel_01");
        }

        private FundModel_01 CreateFundModel()
        {
            return new FundModel_01();
        }
    }
}
