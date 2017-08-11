using System;
using System.Threading.Tasks;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Runners;
using System.Diagnostics;
using System.IO;

namespace ProgFrog.Core.TaskRunning
{
    public class PythonTaskRunner : IProgTaskRunner
    {
        private string _pyInterpreterPath;
        private IInputWriter _inpWriter;
        private IOutputReader _outReader;

        public Process Process { get; private set; }

        public PythonTaskRunner(string interpreterPath, IInputWriter inpWriter, IOutputReader outReader)
        {
            _pyInterpreterPath = interpreterPath;
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

                var startInfo = new ProcessStartInfo
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    FileName = _pyInterpreterPath,
                    CreateNoWindow = true,
                    Arguments = $@"{tempSourceFileName}",
                };

                foreach (var inpParam in task.ParamsAndResults)
                {
                    Process = Process.Start(startInfo);

                    _inpWriter.Configure(this);
                    _outReader.Configure(this);

                    var strParamsSplittedByNewLine = inpParam.Params.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var prm in strParamsSplittedByNewLine)
                    {
                        _inpWriter.Write(inpParam.Params);
                        _inpWriter.Write(Environment.NewLine);
                    }

                    string nextResults = null;

                    await Task.Run(() =>
                    {
                        nextResults = _outReader.Read();
                        Process.WaitForExit();
                    });

                    if (Process.ExitCode != 0)
                    {
                        result.Results.Add(new RunnedTestResult { Results = null, ParamsAndResults = inpParam, IsError = true, ErrorType = RunnedTaskErrorType.RuntimeException });
                    }
                    else
                    {
                        result.Results.Add(new RunnedTestResult { Results = nextResults, ParamsAndResults = inpParam });
                    }
                }
            }
            finally
            {
                File.Delete(tempSourceFileName);
            }

            return result;
        }
    }
}
