using System;
using System.Diagnostics;
using ProgFrog.Core.TaskRunning.Runners;

namespace ProgFrog.Core.TaskRunning
{
    public class StandardInputStreamWriter : IInputWriter
    {
        public IProcess Process { get; private set; }

        public StandardInputStreamWriter(IProcess process)
        {
            Process = process;
        }

        public StandardInputStreamWriter()
        {

        }

        public void Write(string inp)
        {
            if (Process.RedirectStandardInput == false || Process.UseShellExecute != false)
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
