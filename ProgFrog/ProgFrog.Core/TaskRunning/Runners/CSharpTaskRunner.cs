using System;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Compilers;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using ProgFrog.Core.TaskRunning.Compilers.Exceptions;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class CSharpTaskRunner : BaseTaskRunner, IProgTaskRunner
    {
        private ICompiler _compiler;
        private IInputWriter _inpWriter;
        private IOutputReader _outReader;
        private string _execFileName;

        public CSharpTaskRunner(ICompiler compiler, IInputWriter inpWriter, IOutputReader outReader) : base(inpWriter, outReader)
        {
            _compiler = compiler;
        }

        internal Process Process { get; private set; }

        protected override void PrepareForRunning(string codeFileName)
        {
            _execFileName = _compiler.Compile(codeFileName);
        }

        protected override void CleanupAfterRunning()
        {
            if (_execFileName != null)
            {
                File.Delete(_execFileName);
                _execFileName = null;
            }
        }

        protected override ProcessStartInfo GetProcessStartInfo()
        {
            var startInfo = new ProcessStartInfo
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                FileName = _execFileName,
                CreateNoWindow = true
            };

            return startInfo;
        }
    }
}
