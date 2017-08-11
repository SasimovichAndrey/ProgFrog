using ProgFrog.Core.TaskRunning.Runners;

namespace ProgFrog.Core.TaskRunning
{
    public interface IRunnerVisitor
    {
        void Configure(CSharpTaskRunner runner);
        void Configure(PythonTaskRunner runner);
    }
}
