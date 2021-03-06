﻿using AurelienRibon.Ui.SyntaxHighlightBox;
using ProgFrog.Interface.Data;
using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.ResultsChecking;
using ProgFrog.Interface.TaskRunning.Runners;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProgFrog.WpfApp.ViewModel
{
    public class DoTasksViewModel : ViewModelBase
    {
        private IProgrammingTaskRepository _taskRepo;
        private IResultsChecker _resultsChecker;
        private ITaskRunnerProvider _taskRunnerProvider;
        private string _taskStatus;
        private ProgrammingTask _selectedTask;
        private ProgrammingLanguage _programmingLaguage;

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

        public ObservableCollection<ProgrammingLanguage> ProgrammingLanguages { get; set; } = new ObservableCollection<ProgrammingLanguage>();

        public IHighlighter CodeHighlighter
        {
            get
            {
                return HighlighterManager.Instance.Highlighters["CSharp"];
            }
        }

        public ProgrammingLanguage ProgrammingLanguage
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
            foreach (var item in _taskRunnerProvider.GetAvailableLanguages())
            {
                ProgrammingLanguages.Add(item);
            }

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
