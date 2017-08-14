namespace ProgFrog.Interface.TaskRunning
{
    public interface IRunnerVisitor
    {
        void Configure(IProcessTaskRunner runner);
    }
}
