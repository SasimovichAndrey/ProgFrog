using System.IO;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public interface IProcess
    {
        int ExitCode { get; }
        bool RedirectStandardInput { get; }
        bool UseShellExecute { get; }
        bool RedirectStandardOutput { get; }
        StreamWriter StandardInput { get; }
        StreamReader StandardOutput { get; }

        void WaitForExit();
    }
}