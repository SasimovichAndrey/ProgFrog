using ProgFrog.Interface.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgFrog.Interface.Data
{
    public interface IProgrammingTaskRepository
    {
        Task<IEnumerable<ProgrammingTask>> GetAll();
        Task<ProgrammingTask> Create(ProgrammingTask task);
        Task<ProgrammingTask> GetById(IIdentifier identifier);
    }
}
