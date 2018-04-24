using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DCT.TestDataGenerator.Functor
{
    public class FworkCode_02_Tests
    {
        [Fact]
        public void LearnerCount_ProgrammeTypeCount()
        {
            var func = TestHelpers.CreateFunctor<FworkCode_02>();
            var lms = func.LearnerMutators(null).ToList();
            var fms = Enum.GetValues(typeof(ProgType));
            lms.Should().HaveCount(fms.Length - 1, "all progtypes except the higher level 7 (there aren't any)");
        }

        [Fact]
        public void Rulename()
        {
            var func = TestHelpers.CreateFunctor<FworkCode_02>();
            func.RuleName().Should().Be("FworkCode_02");
        }
    }
}
