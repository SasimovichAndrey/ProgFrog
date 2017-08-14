using System;
using System.Threading.Tasks;
using System.Diagnostics;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Runners;
using ProgFrog.Interface.TaskRunning.Compilers.Exceptions;
using ProgFrog.Interface.Model;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public abstract class BaseTaskRunner : IProgTaskRunner, IProcessTaskRunner
    {
        private IInputWriter _inpWriter;
        private IOutputReader _outReader;
        private IFileWriter _fileWriter;
        private IProcessFactory _processFactory;
        private ITempFileProvider _tempFileProvider;

        protected abstract void PrepareForRunning(string codeFileName);
        protected abstract void CleanupAfterRunning();
        protected abstract ProcessStartInfo GetProcessStartInfo();

        public IProcess Process { get; protected set; }

        public BaseTaskRunner(IInputWriter inpWriter, IOutputReader outReader, IFileWriter fileWriter, IProcessFactory processFactory, ITempFileProvider tempFileProvider)
        {
            _inpWriter = inpWriter;
            _outReader = outReader;
            _fileWriter = fileWriter;
            _tempFileProvider = tempFileProvider;
            _processFactory = processFactory;
        }

        public async Task<ProgTaskRunResult> Run(ProgrammingTask task, string userCode)
        {
            // Создание временного файла с кодом
            string tempSourceFileName = _tempFileProvider.CreateNewTempFile();

            var result = new ProgTaskRunResult()
            {
            };
            try
            {
                _fileWriter.Write(userCode, tempSourceFileName);

                foreach (var inpParam in task.ParamsAndResults)
                {
                    PrepareForRunning(tempSourceFileName);
                    var startInfo = GetProcessStartInfo();

                    Process = _processFactory.Start(startInfo);

                    _inpWriter.Configure(this);
                    _outReader.Configure(this);

                    foreach (var prm in inpParam.Params)
                    {
                        _inpWriter.Write(prm);
                        _inpWriter.Write(Environment.NewLine);
                    }

                    string results = null;

                    await Task.Run(() =>
                    {
                        results = _outReader.Read();
                        Process.WaitForExit();
                    });

                    if (Process.ExitCode != 0)
                    {
                        result.Results.Add(new RunnedTestResult { Results = null, ParamsAndResults = inpParam, IsError = true, ErrorType = RunnedTaskErrorType.RuntimeException });
                    }
                    else
                    {
                        result.Results.Add(new RunnedTestResult { Results = results, ParamsAndResults = inpParam });
                    }

                    CleanupAfterRunning();
                }
            }
            catch (CompilationFailedException)
            {
                result.SetRunError(TaskRunErrorType.CompilationFailed);
            }
            finally
            {
                _tempFileProvider.DeleteCurrentTempFile();
            }

            return result;
        }
    }
}
