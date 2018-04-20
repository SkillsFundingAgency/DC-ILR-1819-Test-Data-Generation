using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FundModel_04_Tests
    {
        [Fact]
        public void LearnerCount_ProgrammeTypeCount()
        {
            var func = CreateFundModel();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(ProgType));
            lms.Should().HaveCount(fms.Length - 1, "all progtypes except the higher level 7 (there aren't any)");
        }

        [Fact]
        public void LearnerGenerationOptions_IncludeADL()
        {
            var func = CreateFundModel();
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
            var func = CreateFundModel();
            func.RuleName().Should().Be("FM_04");
        }

        private FundModel_04 CreateFundModel()
        {
            return new FundModel_04();
        }

    }
}
