using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Runners;
using System.Threading.Tasks;

namespace ProgFrog.Core.TaskRunning
{
    public interface IProgTaskRunner
    {
        Task<ProgTaskRunResult> Run(ProgrammingTask task, string userCode);
    }
}
