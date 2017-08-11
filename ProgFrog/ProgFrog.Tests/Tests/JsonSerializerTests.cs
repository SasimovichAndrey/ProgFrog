using NUnit.Framework;
using ProgFrog.Core.Data;
using ProgFrog.Core.Data.Serialization;
using ProgFrog.Core.Model;
using System;
using System.Collections.Generic;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class JsonSerializerTests
    {
        private IModelSerializer<ProgrammingTask> _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new JsonSerializer<ProgrammingTask>();
        }

        [Test]
        public void TestSerialize()
        {
            var obj = new ProgrammingTask
            {
                Description = "test",
                Identifier = new GuidIdentifier(Guid.NewGuid()),
                ParamsAndResults = new List<ParamsAndResults>()
            };

            obj.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "prms1" }, Results = "res1" });

            var serialized = _serializer.Serialize(obj);
            var serializedExpected = "{\"Description\":\"test\",\"ParamsAndResults\":[{\"Params\":[\"prms1\"],\"Results\":\"res1\"}]}";

            Assert.AreEqual(serializedExpected, serialized);
        }

        [Test]
        public void TestDeserialize()
        {
            var obj = new ProgrammingTask
            {
                Description = "test",
                Identifier = new GuidIdentifier(Guid.NewGuid()),
                ParamsAndResults = new List<ParamsAndResults>()
            };

            obj.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "prms1" }, Results = "res1" });

            var serialized= "{\"Description\":\"test\",\"ParamsAndResults\":[{\"Params\":[\"prms1\"],\"Results\":\"res1\"}]}";
            var deserialized = _serializer.Deserialize(serialized);

            Assert.AreEqual(obj, deserialized);
        }
    }
}
