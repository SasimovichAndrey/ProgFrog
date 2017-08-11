using AurelienRibon.Ui.SyntaxHighlightBox;
using ProgFrog.Core.Data;
using ProgFrog.Core.Data.Serialization;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Compilers;
using ProgFrog.Core.TaskRunning.ResultsChecking;
using ProgFrog.Core.TaskRunning.Runners;
using ProgFrog.WpfApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProgFrog.WpfApp.ViewModel
{
    public class DoTasksViewModel : ViewModelBase
    {
        private IProgrammingTaskRepository _taskRepo;
        private IResultsChecker _resultsChecker;
        private ITaskRunnerProvider _taskRunnerProvider;
        private string _taskStatus;
        private ProgrammingTask _selectedTask;
        private ProgrammingLanguageEnum _programmingLaguage;

        // Binding props
        public ObservableCollection<ProgrammingTask> ProgrammingTasks { get; set; } = new ObservableCollection<ProgrammingTask>();

        public string UserCode { get; set; }

        public string TaskStatus
        {
            get { return _taskStatus; }
            set
            {
                _taskStatus = value;
                OnPropertyChanged("TaskStatus");
            }
        }

        public ObservableCollection<ProgrammingLanguageEnum> ProgrammingLanguages { get; set; } = new ObservableCollection<ProgrammingLanguageEnum>();

        public IHighlighter CodeHighlighter
        {
            get
            {
                return HighlighterManager.Instance.Highlighters["CSharp"];
            }
        }

        public ProgrammingLanguageEnum ProgrammingLanguage
        {
            get
            {
                return _programmingLaguage;
            }
            set
            {
                _programmingLaguage = value;
                OnPropertyChanged("ProgrammingLanguage");
            }
        }

        public ProgrammingTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        // ctors
        public DoTasksViewModel(ITaskRunnerProvider taskRunnerProvider, IProgrammingTaskRepository taskRepo, IResultsChecker resultsChecker)
        {
            _taskRepo = taskRepo;
            _taskRunnerProvider = taskRunnerProvider;
            _resultsChecker = resultsChecker;

            foreach(var item in _taskRunnerProvider.GetAvailableLanguages())
            {
                ProgrammingLanguages.Add(item);
            }
        }

        // actions
        public async Task RunSelectedTask()
        {
            var runner = _taskRunnerProvider.GetRunner(ProgrammingLanguage);
            var runTask = runner.Run(SelectedTask, UserCode);

            TaskStatus = "Task running";

            var runResults = await runTask;

            if (!runResults.IsRunError)
            {
                var checkResults = _resultsChecker.Check(runResults.Results);

                if (checkResults.IsSuccessfull)
                {
                    TaskStatus = "OK";
                }
                else
                {
                    TaskStatus = GetErrorMessage(checkResults.ErrorType);
                }
            }
            else
            {
                TaskStatus = GetRunErrorMessage(runResults.ErrorType);
            }
        }

        public async void Initialize(object sender, EventArgs e)
        {
            var tasks = await _taskRepo.GetAll();
            foreach (var task in tasks)
            {
                ProgrammingTasks.Add(task);
            }
        }

        private string GetRunErrorMessage(TaskRunErrorType? errorType)
        {
            switch (errorType)
            {
                case TaskRunErrorType.CompilationFailed:
                    return "Compilation failed";
                default:
                    throw new ApplicationException("Unknown run error type");
            }
        }

        private string GetErrorMessage(ResultFailureType? errorType)
        {
            switch (errorType)
            {
                case ResultFailureType.WrongResults:
                    return "Несовпадение результов с правильными";
                case ResultFailureType.RuntimeException:
                    return "Ошибка времени выполнения";
                default:
                    throw new ApplicationException("Unknown failure type");
            }
        }
    }
}
