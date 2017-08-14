namespace ProgFrog.Interface.TaskRunning
{
    public interface IOutputReader : IRunnerVisitor
    {
        string Read();
    }
}
