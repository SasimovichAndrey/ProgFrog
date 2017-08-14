using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning.Runners;
using System.Threading.Tasks;

namespace ProgFrog.Interface.TaskRunning
{
    public interface IProgTaskRunner
    {
        Task<ProgTaskRunResult> Run(ProgrammingTask task, string userCode);
    }
}
