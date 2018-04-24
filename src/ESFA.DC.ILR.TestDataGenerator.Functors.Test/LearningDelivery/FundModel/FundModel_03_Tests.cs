using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_03_Tests
    {
        [Fact]
        public void LearnerCount_FundModelCount()
        {
            var func = TestHelpers.CreateFunctor<FundModel_03>();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(FundModel));
            lms.Should().HaveCount(fms.Length);
        }

        [Fact]
        public void LearnerGenerationOptions_IncludeADL()
        {
            var func = TestHelpers.CreateFunctor<FundModel_03>();
            var lms = func.LearnerMutators(null).ToList();
            lms.ForEach(s =>
            {
                GenerationOptions options = new GenerationOptions();
                s.DoMutateOptions(options);
                options.LD.IncludeADL.Should().BeTrue();
            });
        }

        [Fact]
        public void Rulename()
        {
            var func = TestHelpers.CreateFunctor<FundModel_03>();
            func.RuleName().Should().Be("FundModel_03");
        }
    }
}
