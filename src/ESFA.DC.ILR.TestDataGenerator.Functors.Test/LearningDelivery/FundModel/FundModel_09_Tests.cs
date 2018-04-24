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
            var func = TestHelpers.CreateFunctor<FundModel_09>();
            func.RuleName().Should().Be("FundModel_09");
        }

        [Fact]
        public void LearnerMutatorCount_StandardsAndExclusions()
        {
            var func = TestHelpers.CreateFunctor<FundModel_09>();
            var lms = func.LearnerMutators(null).ToList();
            var magicNumber = 2;
            lms.Should().HaveCount(magicNumber);
        }
    }
}
