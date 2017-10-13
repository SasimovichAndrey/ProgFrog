using Moq;
using NUnit.Framework;
using ProgFrog.Core.TaskRunning.Runners;
using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Compilers;
using ProgFrog.Interface.TaskRunning.Runners;
using ProgFrog.Tests.Tests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class CSharpTaskRunnerTests : RunnersTestBase
    {
        CSharpTaskRunner _runner;
        Mock<ICompiler> _compilerMock;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _compilerMock = new Mock<ICompiler>();
            _runner = new CSharpTaskRunner(_compilerMock.Object, _inputWriterMock.Object, _outReaderMock.Object, _fileWriterMock.Object, _processFactoryMock.Object,
                _tempFileProviderMock.Object);
        }

        [Test]
        public async Task TestSingleParam()
        {
            // arrange
            var testResult = $"5{Environment.NewLine}";
            var tempFileName = "tempFile";
            _outReaderMock.Setup(or => or.Read()).Returns(testResult);
            _processFactoryMock.Setup(f => f.Start(It.IsAny<ProcessStartInfo>())).Returns(new Mock<IProcess>().Object);
            _tempFileProviderMock.Setup(p => p.CreateNewTempFile()).Returns(tempFileName);
            var task = new ProgrammingTask
            {
                Description = "x+2",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            task.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "3" }, Results = testResult });
            string code = "code";

            // act
            var res = await _runner.Run(task, code);

            // assert
            _fileWriterMock.Verify(fw => fw.Write(code, tempFileName));
            _compilerMock.Verify(c => c.Compile(tempFileName));
            _tempFileProviderMock.Verify(tfp => tfp.CreateNewTempFile(), Times.Exactly(task.ParamsAndResults.Count()));
            _tempFileProviderMock.Verify(tfp => tfp.DeleteCurrentTempFile(), Times.Exactly(task.ParamsAndResults.Count()));
            _inputWriterMock.Verify(iw => iw.Configure(_runner), Times.Exactly(task.ParamsAndResults.Count()));
            _outReaderMock.Verify(or => or.Configure(_runner), Times.Exactly(task.ParamsAndResults.Count()));
            _outReaderMock.Verify(or => or.Read(), Times.Exactly(task.ParamsAndResults.Count()));

            Assert.AreEqual(testResult, res.Results.First().Results);
        }

        [Test]
        public async Task TestMultipleParams()
        {
            var task = new ProgrammingTask
            {
                Description = "x+y",
                ParamsAndResults = new List<ParamsAndResults>()
            };
            task.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "3", "4" } });


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
            task.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "3" } });
            task.ParamsAndResults.Add(new ParamsAndResults { Params = new List<string>() { "4" } });


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
