namespace ProgFrog.Core.TaskRunning.Runners
{
    public interface ITempFileProvider
    {
        string CreateNewTempFile();
        void DeleteCurrentTempFile();
    }
}
