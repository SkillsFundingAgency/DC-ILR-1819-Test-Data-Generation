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
            var func = CreateFundModel();
            func.RuleName().Should().Be("FundModel_05");
        }

        [Fact]
        public void OverrideStartDate()
        {
            var func = CreateFundModel();
            var funcy = func.LearnerMutators(null).ToList();
            GenerationOptions options = new GenerationOptions();
            funcy[0].DoMutateOptions(options);
            options.LD.OverrideLearnStartDate.Should().BeOnOrAfter(DateTime.Parse("2017-MAY-01"));
        }

        private FundModel_05 CreateFundModel()
        {
            return new FundModel_05();
        }

    }
}
