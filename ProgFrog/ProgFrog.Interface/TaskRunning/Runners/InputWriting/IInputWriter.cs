namespace ProgFrog.Interface.TaskRunning
{
    public interface IInputWriter : IRunnerVisitor
    {
        void Write(string inp);
    }
}