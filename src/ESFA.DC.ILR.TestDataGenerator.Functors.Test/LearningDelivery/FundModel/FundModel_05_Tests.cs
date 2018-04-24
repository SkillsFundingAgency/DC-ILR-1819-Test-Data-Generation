using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_05_Tests
    {
        [Fact]
        public void Rulename()
        {
            var func = TestHelpers.CreateFunctor<FundModel_05>();
            func.RuleName().Should().Be("FundModel_05");
        }

        [Fact]
        public void OverrideStartDate()
        {
            var func = TestHelpers.CreateFunctor<FundModel_05>();
            var funcy = func.LearnerMutators(null).ToList();
            GenerationOptions options = new GenerationOptions();
            funcy[0].DoMutateOptions(options);
            options.LD.OverrideLearnStartDate.Should().BeOnOrAfter(DateTime.Parse("2017-MAY-01"));
        }
    }
}
