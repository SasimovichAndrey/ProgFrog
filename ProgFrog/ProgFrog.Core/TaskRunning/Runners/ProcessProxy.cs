using System;
using System.Diagnostics;
using System.IO;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class ProcessProxy : IProcess
    {
        private Process _process;

        public ProcessProxy(Process process)
        {
            this._process = process;
        }

        public int ExitCode { get { return _process.ExitCode; } }

        public bool RedirectStandardInput
        {
            get
            {
                return _process.StartInfo.RedirectStandardInput;
            }
        }

        public bool UseShellExecute
        {
            get
            {
                return _process.StartInfo.UseShellExecute;
            }
        }

        public bool RedirectStandardOutput
        {
            get
            {
                return _process.StartInfo.RedirectStandardOutput;
            }
        }

        public StreamWriter StandardInput
        {
            get
            {
                return _process.StandardInput;
            }
        }

        public StreamReader StandardOutput
        {
            get
            {
                return _process.StandardOutput;
            }
        }

        public void WaitForExit()
        {
            _process.WaitForExit();
        }
    }
}