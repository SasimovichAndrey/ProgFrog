using ProgFrog.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgFrog.Core.Data
{
    public interface IProgrammingTaskRepository
    {
        Task<IEnumerable<ProgrammingTask>> GetAll();
        Task<ProgrammingTask> Create(ProgrammingTask task);
    }
}
