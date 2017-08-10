using NUnit.Framework;
using ProgFrog.Core.Data;
using ProgFrog.Core.Data.Serialization;
using ProgFrog.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class TaskFileRepositoryTests : TestsBase
    {
        private IProgrammingTaskRepository _repo;

        [SetUp]
        public void Setup()
        {
            this._repo = new FileProgramminTaskRepository(new JsonSerializer<ProgrammingTask>(), Path.Combine(GetTestDataDirectoryPath(), "ProgTasks"));
        }

        [Test]
        public async Task Test()
        {
            var expected = new List<ProgrammingTask>();
            var first = new ProgrammingTask
            {
                Identifier = new GuidIdentifier(new Guid("0a1d9e59-ad14-4e5a-8afa-c5fc987649cf")),
                Description = "test1",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            expected.Add(first);
            first.ParamsAndResults.Add(new ParamsAndResults { Params = "prms1", Results = "res1" });

            var second = new ProgrammingTask
            {
                Identifier = new GuidIdentifier(new Guid("462751a2-faf3-4342-a18d-1bbc12be0a86")),
                Description = "test2",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            expected.Add(second);
            second.ParamsAndResults.Add(new ParamsAndResults { Params = "prms2", Results = "res2" });

            var actual = await _repo.GetAll();

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count());

            Assert.AreEqual(first, actual.ElementAt(0));
            Assert.AreEqual(first.Identifier.StringPresentation, actual.ElementAt(0).Identifier.StringPresentation);

            Assert.AreEqual(second, actual.ElementAt(1));
            Assert.AreEqual(second.Identifier.StringPresentation, actual.ElementAt(1).Identifier.StringPresentation);
        }
    }
}
