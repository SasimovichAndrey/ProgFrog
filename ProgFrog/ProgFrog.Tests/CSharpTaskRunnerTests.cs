using NUnit.Framework;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Runners;
using System;
using System.Collections.Generic;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class CSharpTaskRunnerTests : TestsBase
    {
        IProgTaskRunner _runner;

        [SetUp]
        public void Setup()
        {
            _runner = new CSharpTaskRunner();
        }

        [Test]
        public void Test()
        {
            var task = new ProgrammingTask
            {
                Description = "x+2",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            task.ParamsAndResults.Add(new ParamsAndResults { Params = "3", Results = "5" });


            string code = @"using System;

                namespace ConsoleDraft
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            var x = int.Parse(Console.ReadLine());
                            Console.WriteLine(x + 2);
                        }
                    }
                }";

            var res = _runner.Run(task, code);

            var expectedRes = new RunnedTaskResult
            {
                Results = $"4{Environment.NewLine}"
            };

            Assert.AreEqual(expectedRes.Results, res.Results);
        }
    }
}
