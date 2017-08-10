using System;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Compilers;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using ProgFrog.Core.TaskRunning.Compilers.Exceptions;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class CSharpTaskRunner : IProgTaskRunner
    {
        private ICompiler _compiler;
        private IInputWriter _inpWriter;
        private IOutputReader _outReader;

        public CSharpTaskRunner(ICompiler compiler, IInputWriter inpWriter, IOutputReader outReader)
        {
            _compiler = compiler;
            _inpWriter = inpWriter;
            _outReader = outReader;
        }

        internal Process Process { get; private set; }

        public async Task<ProgTaskRunResult> Run(ProgrammingTask task, string userCode)
        {
            // Создание временного файла с кодом
            string tempSourceFileName = Path.GetTempFileName();
            string tempExecutableFileName = null;
            var result = new ProgTaskRunResult()
            {
            };
            try
            {
                using (var writer = new StreamWriter(File.OpenWrite(tempSourceFileName)))
                {
                    writer.Write(userCode);
                }

                // Создание исполняемого файла
                tempExecutableFileName = _compiler.Compile(tempSourceFileName);

                var startInfo = new ProcessStartInfo
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    FileName = tempExecutableFileName,
                    CreateNoWindow = true
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
            catch(CompilationFailedException ex)
            {
                result.SetRunError(TaskRunErrorType.CompilationFailed);
            }
            finally
            {
                File.Delete(tempSourceFileName);
                if(tempExecutableFileName != null)
                {
                    File.Delete(tempExecutableFileName);
                }
            }

            return result;
        }
    }
}
