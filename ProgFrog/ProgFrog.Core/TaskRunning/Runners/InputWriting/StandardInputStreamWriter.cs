using System;
using System.Diagnostics;
using ProgFrog.Core.TaskRunning.Runners;

namespace ProgFrog.Core.TaskRunning
{
    public class StandardInputStreamWriter : IInputWriter
    {
        public Process Process { get; private set; }

        public StandardInputStreamWriter(Process process)
        {
            Process = process;
        }

        public StandardInputStreamWriter()
        {

        }

        public void Write(string inp)
        {
            if (Process.StartInfo.RedirectStandardInput == false || Process.StartInfo.UseShellExecute != false)
            {
                throw new ApplicationException("Cannot read from this process std output");
            }

            Process.StandardInput.Write(inp);
        }

        public void Configure(BaseTaskRunner runner)
        {
            Process = runner.Process;
        }
    }
}
