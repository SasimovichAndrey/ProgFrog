using System;
using System.Threading.Tasks;
using ProgFrog.Core.Model;
using System.IO;
using ProgFrog.Core.TaskRunning.Compilers.Exceptions;
using System.Diagnostics;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public abstract class BaseTaskRunner : IProgTaskRunner
    {
        private IInputWriter _inpWriter;
        private IOutputReader _outReader;

        protected abstract void PrepareForRunning(string codeFileName);
        protected abstract void CleanupAfterRunning();
        protected abstract ProcessStartInfo GetProcessStartInfo();

        public Process Process { get; protected set; }

        public BaseTaskRunner(IInputWriter inpWriter, IOutputReader outReader)
        {
            _inpWriter = inpWriter;
            _outReader = outReader;
        }

        public async Task<ProgTaskRunResult> Run(ProgrammingTask task, string userCode)
        {
            // Создание временного файла с кодом
            string tempSourceFileName = Path.GetTempFileName();

            var result = new ProgTaskRunResult()
            {
            };
            try
            {
                using (var writer = new StreamWriter(File.OpenWrite(tempSourceFileName)))
                {
                    writer.Write(userCode);
                }

                foreach (var inpParam in task.ParamsAndResults)
                {
                    PrepareForRunning(tempSourceFileName);
                    var startInfo = GetProcessStartInfo();

                    Process = Process.Start(startInfo);

                    _inpWriter.Configure(this);
                    _outReader.Configure(this);

                    var strParamsSplittedByNewLine = inpParam.Params.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var prm in strParamsSplittedByNewLine)
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
                File.Delete(tempSourceFileName);
            }

            return result;
        }
    }
}
