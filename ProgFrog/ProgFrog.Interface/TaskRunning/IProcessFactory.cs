using System.Diagnostics;

namespace ProgFrog.Interface.TaskRunning.Runners
{
    public interface IProcessFactory
    {
        IProcess Start(ProcessStartInfo startInfo);
    }
}