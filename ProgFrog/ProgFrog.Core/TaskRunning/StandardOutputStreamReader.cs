using System;
using System.Diagnostics;

namespace ProgFrog.Core.TaskRunning
{
    public class StandardOutputStreamReader : IOutputReader
    {
        public Process Process { get; private set; }

        public StandardOutputStreamReader(Process process)
        {
            Process = process;
        }

        public string Read()
        {
            if (Process.StartInfo.RedirectStandardOutput == false || Process.StartInfo.UseShellExecute != false)
            {
                throw new ApplicationException("Cannot read from this process std output");
            }

            return Process.StandardOutput.ReadToEnd();
        }
    }
}
