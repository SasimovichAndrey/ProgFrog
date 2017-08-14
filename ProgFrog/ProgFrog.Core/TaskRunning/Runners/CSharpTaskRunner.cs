using System.IO;
using System.Diagnostics;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Compilers;
using ProgFrog.Interface.TaskRunning.Runners;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class CSharpTaskRunner : BaseTaskRunner, IProgTaskRunner
    {
        private ICompiler _compiler;
        private IInputWriter _inpWriter;
        private IOutputReader _outReader;
        private string _execFileName;

        public CSharpTaskRunner(ICompiler compiler, IInputWriter inpWriter, IOutputReader outReader, IFileWriter fileWriter, IProcessFactory processFactory, ITempFileProvider tempFileProvider) 
            : base(inpWriter, outReader, fileWriter, processFactory, tempFileProvider)
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
