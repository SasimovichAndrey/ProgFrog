using ProgFrog.Interface.Data;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Runners;
using ProgFrog.WebApi.Filters;
using ProgFrog.WebApi.ViewModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProgFrog.WebApi.Controllers
{
    public class TaskRunnerController : ApiController
    {
        private ITaskRunnerProvider _taskRunnerProvider;
        private IProgrammingTaskRepository _programmingTaskRepository;

        public TaskRunnerController(ITaskRunnerProvider taskRunnerProvider, IProgrammingTaskRepository programmingTaskRepository)
        {
            _taskRunnerProvider = taskRunnerProvider;
            _programmingTaskRepository = programmingTaskRepository;
        }

        [HttpPost]
        public async Task<IHttpActionResult> RunTask(RunTaskRequest req)
        {
            var taskRunner = _taskRunnerProvider.GetRunner(req.ProgrammingLanguage);
            var dbTask = await _programmingTaskRepository.GetById(req.Task.Identifier);
            if(dbTask != null)
            {
                var result = await taskRunner.Run(dbTask, req.UserCode);

                return Ok(result);
            }
            else
            {
                return BadRequest($"Programming task with id={req.Task.Identifier} doesn't exist");
            }
        }
    }
}