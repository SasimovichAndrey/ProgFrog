using Moq;
using NUnit.Framework;
using ProgFrog.Core.Data;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.ResultsChecking;
using ProgFrog.Core.TaskRunning.Runners;
using ProgFrog.WpfApp.Infrastructure;
using ProgFrog.WpfApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Tests.Tests
{
    [TestFixture]
    public class DoTasksViewModelTests
    {
        private DoTasksViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var progTaskRunResult = new ProgTaskRunResult();
            progTaskRunResult.SetRunError(TaskRunErrorType.CompilationFailed);

            var runnerMockObj = new Mock<IProgTaskRunner>();
            runnerMockObj.Setup(m => m.Run(It.IsAny<ProgrammingTask>(), It.IsAny<string>())).Returns(() =>
                Task.FromResult<ProgTaskRunResult>(progTaskRunResult));

            var runnerProviderMock = new Mock<ITaskRunnerProvider>();
            runnerProviderMock.Setup(m => m.GetRunner(It.IsAny<ProgrammingLanguage>())).Returns(() => { return runnerMockObj.Object; });

            var taskRepoMock = new Mock<IProgrammingTaskRepository>();
            var checkerMock = new Mock<IResultsChecker>();

            _viewModel = new DoTasksViewModel(runnerProviderMock.Object, taskRepoMock.Object, checkerMock.Object);
        }

        [Test]
        public async Task TestTasksStatusCompilationFailed()
        {
            await _viewModel.RunSelectedTask();

            Assert.AreEqual(_viewModel.TaskStatus, "Compilation failed");
        }
    }
}
