﻿using ProgFrog.Core.Data;
using ProgFrog.Core.Model;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Runners;
using ProgFrog.Core.TaskRunning.Compilers;
using ProgFrog.Core.TaskRunning.ResultsChecking;
using System.ComponentModel;
using ProgFrog.Core.Data.Serialization;
using ProgFrog.WpfApp.ViewModel;
using ProgFrog.WpfApp.Infrastructure;

namespace ProgFrog.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DoTasksViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            IModelSerializer<ProgrammingTask> serializer = new JsonSerializer<ProgrammingTask>();
            var dataDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "App_Data");
            var repo = new FileProgramminTaskRepository(serializer, dataDirectory);


            var taskRunnerProvider = new TaskRunnerProvider();
            string csharpCompilerPath = @"c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe";
            taskRunnerProvider.RegisterRunner(new CSharpTaskRunner(new CSharpCompiler(csharpCompilerPath), new StandardInputStreamWriter(), new StandardOutputStreamReader()), ProgrammingLanguageEnum.CSharp);
            var resultsChecker = new ResultsChecker();
            var vm = new DoTasksViewModel(taskRunnerProvider, repo, resultsChecker);
            this.DataContext = vm;
            this.ViewModel = vm;
            this.Loaded += vm.Initialize;
        }

        public void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedTask = ((ListBox)sender).SelectedItem as ProgrammingTask;
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.RunSelectedTask();
        }
    }
}