using NUnit.Framework;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgFrog.Tests.Tests
{
    [TestFixture]
    public class PythonTaskRunnerTests 
    {
        IProgTaskRunner _runner;

        [SetUp]
        public void Setup()
        {
            _runner = new PythonTaskRunner(@"c:\Python27\python.exe", new StandardInputStreamWriter(), new StandardOutputStreamReader());
        }

        [Test]
        public async Task TestSingleParam()
        {
            var task = new ProgrammingTask
            {
                Description = "x+2",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            task.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "3" }, Results = "5" });


            string code = @"x = input()
print x+2";

            var res = await _runner.Run(task, code);

            var expectedRes = new RunnedTestResult
            {
                Results = $"5{Environment.NewLine}"
            };

            Assert.AreEqual(expectedRes.Results, res.Results.First().Results);
        }
    }
}
