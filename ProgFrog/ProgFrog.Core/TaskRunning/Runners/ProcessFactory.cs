using System;
using System.Diagnostics;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class ProcessFactory : IProcessFactory
    {
        public IProcess Start(ProcessStartInfo startInfo)
        {
            return new ProcessProxy(Process.Start(startInfo));
        }
    }
}
