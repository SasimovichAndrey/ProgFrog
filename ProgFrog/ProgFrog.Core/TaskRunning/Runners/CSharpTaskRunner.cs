using System;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Compilers;
using System.IO;
using System.Diagnostics;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class CSharpTaskRunner : IProgTaskRunner
    {
        private ICompiler _compiler;

        public RunnedTaskResult Run(ProgrammingTask task, string userCode)
        {
            // Создание временного файла с кодом
            string tempSourceFileName = Path.GetTempFileName();
            string tempExecutableFileName = null;
            try
            {
                using (var writer = new StreamWriter(File.OpenWrite(tempSourceFileName)))
                {
                    writer.Write(userCode);
                }

                // Создание исполняемого файла
                var programFile = _compiler.Compile(tempSourceFileName);

                var startInfo = new ProcessStartInfo
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                var process = Process.Start(startInfo);
                var inputWriter = new StandardInputStreamWriter(process);
                
            }
            finally
            {
                File.Delete(tempSourceFileName);
                if(tempExecutableFileName != null)
                {
                    File.Delete(tempExecutableFileName);
                }
            }

            throw new NotImplementedException();
        }
    }
}
