using ProgFrog.Interface.Data;
using ProgFrog.Interface.Model;
using ProgFrog.WebApi.ViewModel;
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

        public async Task<IEnumerable<ProgrammingTaskViewModel>> Get()
        {
            var progTasks = await _progTaskRepo.GetAll();

            var vms = AutoMapper.Mapper.Map<IEnumerable<ProgrammingTaskViewModel>>(progTasks);

            return vms;
        }
    }
}
