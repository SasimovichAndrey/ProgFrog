using ProgFrog.Core.Model;

namespace ProgFrog.Core.TaskRunning
{
    public interface IProgTaskRunner
    {
        RunnedTaskResult Run(ProgrammingTask task, string userCode);
    }
}
