namespace ProgFrog.Interface.TaskRunning.Runners
{
    public interface ITempFileProvider
    {
        string CreateNewTempFile();
        void DeleteCurrentTempFile();
    }
}
