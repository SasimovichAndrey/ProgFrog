using ProgFrog.Interface.Data;
using ProgFrog.Interface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProgFrog.WebApi.Controllers
{
    public class ProgrammingTasksController : ApiController
    {
        private IProgrammingTaskRepository _progTaskRepo;

        public ProgrammingTasksController(IProgrammingTaskRepository progTaskRepo)
        {
            _progTaskRepo = progTaskRepo;
        }

        public async Task<IEnumerable<ProgrammingTask>> Get()
        {
            var progTasks = await _progTaskRepo.GetAll();

            return progTasks;
        }
    }
}
