using Moq;
using NUnit.Framework;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Runners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProgFrog.Tests.Tests
{
    [TestFixture]
    public class PythonTaskRunnerTests : RunnersTestBase
    {
        PythonTaskRunner _runner;

        [SetUp]
        public void Setup()
        {
            _runner = new PythonTaskRunner(@"c:\Python27\python.exe", _inputWriterMock.Object, _outReaderMock.Object, _fileWriterMock.Object, _processFactoryMock.Object, _tempFileProviderMock.Object);
        }

        [Test]
        public async Task TestSingleParam()
        {
            // arrange
            var testResult = $"5{Environment.NewLine}";
            _outReaderMock.Setup(or => or.Read()).Returns(testResult);
            _processFactoryMock.Setup(f => f.Start(It.IsAny<ProcessStartInfo>())).Returns(new Mock<IProcess>().Object);
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
            _fileWriterMock.Verify(fw => fw.Write(code, It.IsAny<string>()));
            _tempFileProviderMock.Verify(tfp => tfp.CreateNewTempFile(), Times.Exactly(task.ParamsAndResults.Count()));
            _tempFileProviderMock.Verify(tfp => tfp.DeleteCurrentTempFile(), Times.Exactly(task.ParamsAndResults.Count()));
            _inputWriterMock.Verify(iw => iw.Configure(_runner), Times.Exactly(task.ParamsAndResults.Count()));
            _outReaderMock.Verify(or => or.Configure(_runner), Times.Exactly(task.ParamsAndResults.Count()));
            _outReaderMock.Verify(or => or.Read(), Times.Exactly(task.ParamsAndResults.Count()));

            Assert.AreEqual(testResult, res.Results.First().Results);
        }
    }
}
