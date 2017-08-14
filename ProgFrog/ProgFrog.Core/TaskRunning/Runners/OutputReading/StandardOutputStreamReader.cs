using System;
using System.Diagnostics;
using ProgFrog.Core.TaskRunning.Runners;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Runners;

namespace ProgFrog.Core.TaskRunning
{
    public class StandardOutputStreamReader : IOutputReader
    {
        public IProcess Process { get; private set; }

        public StandardOutputStreamReader(IProcess process)
        {
            Process = process;
        }

        public StandardOutputStreamReader()
        {

        }

        public string Read()
        {
            if (Process.RedirectStandardOutput == false || Process.UseShellExecute != false)
            {
                throw new ApplicationException("Cannot read from this process std output");
            }

            return Process.StandardOutput.ReadToEnd();
        }

        public void Configure(IProcessTaskRunner runner)
        {
            Process = runner.Process;
        }
    }
}
