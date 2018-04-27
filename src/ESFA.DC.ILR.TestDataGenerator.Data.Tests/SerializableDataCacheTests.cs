using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.IO;
using Newtonsoft.Json.Linq;

namespace DCT.TestDataGenerator
{
    public class SerializableDataCacheTests
    {
        [Fact]
        public void CTOR_BlankCollectionsCreated()
        {
            SerializableDataCache lhs = new SerializableDataCache();
            Type t = lhs.GetType();
            var fields = t.GetFields();
            foreach (var field in fields)
            {
                field.GetValue(lhs).Should().NotBeNull("{0} is null and all collection fields required to be instantiated for streaming", field.Name);
            }
        }

        [Fact]
        public void Serialize_Deserialize_Same()
        {
            SerializableDataCache lhs = new SerializableDataCache();
            lhs.CreateFromStaticData();
            SerializableDataCache rhs = new SerializableDataCache();
            string result = string.Empty;
            using (var stream = new MemoryStream(1024))
            {
                lhs.WriteToStream(stream);
                result = Encoding.UTF8.GetString(stream.ToArray());
                stream.Position = 0;
                rhs.ReadFromStream(stream);
            }

            rhs._apprenticeShipAims.Should().BeEquivalentTo(lhs._apprenticeShipAims);
        }

        [Fact]
        public void DeserializeDynamicObjects_ApprenticeshipAimsFindable()
        {
            SerializableDataCache lhs = new SerializableDataCache();
            lhs.CreateFromStaticData();
            string result = string.Empty;
            using (var stream = new MemoryStream(1024))
            {
                lhs.WriteToStream(stream);
                result = Encoding.UTF8.GetString(stream.ToArray());
            }

            dynamic rhs = JObject.Parse(result);
            string intermediateAim = rhs["_apprenticeShipAims"][ProgType.IntermediateLevelApprenticeship.ToString()]["LearningDelivery"]["LearnAimRef"];
            intermediateAim.Should().Be(lhs._apprenticeShipAims[ProgType.IntermediateLevelApprenticeship].LearningDelivery.LearnAimRef);
        }
    }
}
