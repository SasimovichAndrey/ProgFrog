using System;
using System.Threading.Tasks;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Runners;
using System.Diagnostics;
using System.IO;

namespace ProgFrog.Core.TaskRunning
{
    public class PythonTaskRunner : BaseTaskRunner, IProgTaskRunner
    {
        private string _pyInterpreterPath;
        private string _codeFileName;

        public Process Process { get; private set; }

        public PythonTaskRunner(string interpreterPath, IInputWriter inpWriter, IOutputReader outReader) : base(inpWriter, outReader)
        {
            _pyInterpreterPath = interpreterPath;
        }

        protected override void PrepareForRunning(string codeFileName)
        {
            _codeFileName = codeFileName;
        }

        protected override void CleanupAfterRunning()
        {
        }

        protected override ProcessStartInfo GetProcessStartInfo()
        {
            return new ProcessStartInfo
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                FileName = _pyInterpreterPath,
                CreateNoWindow = true,
                Arguments = $@"{_codeFileName}"
            };
        }
    }
}
