using ProgFrog.Interface.TaskRunning.Runners;

namespace ProgFrog.Interface.TaskRunning
{
    public interface IProcessTaskRunner
    {
        IProcess Process { get; }
    }
}
