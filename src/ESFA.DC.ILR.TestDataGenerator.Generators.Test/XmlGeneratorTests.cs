using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCT.TestDataGenerator;
using FluentAssertions;
using Xunit;

namespace DCT.TestDataGenerator.Test
{
    public class XmlGeneratorTests
    {
        [Fact]
        public void CreateAllXml_NullArgumentToException()
        {
            int UKPRN = 8;
            XmlGenerator generator = new XmlGenerator(null, UKPRN);
            Action call = () => { generator.CreateAllXml(null, 1, XmlGenerator.ESFA201819Namespace); };
            call.Should().Throw<NullReferenceException>();
        }
    }
}
