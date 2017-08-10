using NUnit.Framework;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Compilers;
using ProgFrog.Core.TaskRunning.Runners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class CSharpTaskRunnerTests : CompilerTestBase
    {
        IProgTaskRunner _runner;

        [SetUp]
        public void Setup()
        {
            _runner = new CSharpTaskRunner(new CSharpCompiler(_cSharpCompPath), new StandardInputStreamWriter(), new StandardOutputStreamReader());
        }

        [Test]
        public async Task TestSingleParam()
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

            var res = await _runner.Run(task, code);

            var expectedRes = new RunnedTestResult
            {
                Results = $"5{Environment.NewLine}"
            };

            Assert.AreEqual(expectedRes.Results, res.Results.First().Results);
        }

        [Test]
        public async Task TestMultipleParams()
        {
            var task = new ProgrammingTask
            {
                Description = "x+y",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            task.ParamsAndResults.Add(new ParamsAndResults { Params = $"3{Environment.NewLine}4" });


            string code = @"using System;

                namespace ConsoleDraft
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            var x = int.Parse(Console.ReadLine());
                            var y = int.Parse(Console.ReadLine());
                            Console.WriteLine(x + y);
                        }
                    }
                }";

            var res = await _runner.Run(task, code);

            var expectedRes = new RunnedTestResult
            {
                Results = $"7{Environment.NewLine}"
            };

            Assert.AreEqual(expectedRes.Results, res.Results.First().Results);
        }

        [Test]
        public async Task TestMultiplePrmsResultsPairs()
        {
            var task = new ProgrammingTask
            {
                Description = "x+2",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            task.ParamsAndResults.Add(new ParamsAndResults { Params = "3"});
            task.ParamsAndResults.Add(new ParamsAndResults { Params = "4"});


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

            var res = await _runner.Run(task, code);

            var expectedRes1 = new RunnedTestResult
            {
                Results = $"5{Environment.NewLine}"
            };
            var expectedRes2 = new RunnedTestResult
            {
                Results = $"6{Environment.NewLine}"
            };

            Assert.AreEqual(expectedRes1.Results, res.Results.First().Results);
            Assert.AreEqual(expectedRes2.Results, res.Results.ElementAt(1).Results);
        }
    }
}
