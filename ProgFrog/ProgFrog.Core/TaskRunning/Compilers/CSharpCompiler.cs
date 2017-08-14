using ProgFrog.Interface.TaskRunning.Compilers;
using ProgFrog.Interface.TaskRunning.Compilers.Exceptions;
using System;
using System.Diagnostics;
using System.IO;

namespace ProgFrog.Core.TaskRunning.Compilers
{
    public class CSharpCompiler : ICompiler
    {
        private string _compilerPath;

        public CSharpCompiler(string compilerPath)
        {
            _compilerPath = compilerPath;
        }

        public string Compile(string sourceCodeFileName)
        {
            var dir = Path.GetDirectoryName(sourceCodeFileName);
            var fileName = Path.GetFileNameWithoutExtension(sourceCodeFileName);
            var execFileName = Path.Combine(dir, fileName) + ".exe";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = _compilerPath,
                Arguments = $"/out:\"{execFileName}\" \"{sourceCodeFileName}\"",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var compProc = Process.Start(startInfo);
            var stdError = compProc.StandardError;
            var stdOut = compProc.StandardOutput;

            compProc.WaitForExit();

            var error = stdError.ReadToEnd();
            var outp = stdOut.ReadToEnd();


            if(compProc.ExitCode != 0)
            {
                throw new CompilationFailedException("Cant compile c# program");
            }

            return execFileName;
        }
    }
}
