using System.Diagnostics;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public interface IProcessFactory
    {
        IProcess Start(ProcessStartInfo startInfo);
    }
}