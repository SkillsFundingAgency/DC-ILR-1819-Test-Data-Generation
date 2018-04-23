using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_09_Tests
    {
        [Fact]
        public void Rulename()
        {
            var func = CreateFundModel();
            func.RuleName().Should().Be("FundModel_09");
        }

        [Fact]
        public void LearnerMutatorCount_StandardsAndExclusions()
        {
            var func = CreateFundModel();
            var lms = func.LearnerMutators(null).ToList();
            var magicNumber = 2;
            lms.Should().HaveCount(magicNumber);
        }

        private FundModel_09 CreateFundModel()
        {
            return new FundModel_09();
        }

    }
}
