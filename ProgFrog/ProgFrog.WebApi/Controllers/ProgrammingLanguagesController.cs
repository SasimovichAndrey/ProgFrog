using ProgFrog.Interface.TaskRunning;
using ProgFrog.WebApi.ViewModel;
using ProgFrog.WebApi.ViewModel.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProgFrog.WebApi.Controllers
{
    public class ProgrammingLanguagesController : ApiController
    {
        private readonly ITaskRunnerProvider _taskRunnerProvider;

        public ProgrammingLanguagesController(ITaskRunnerProvider taskRunnerProvider)
        {
            this._taskRunnerProvider = taskRunnerProvider;
        }

        [HttpGet]
        public IEnumerable<ProgrammingLanguageViewModel> Get()
        {
            var langEnums = _taskRunnerProvider.GetAvailableLanguages();
            var vms = langEnums.Select(enm => MappingsHelper.MapFromProgrammingLanguage(enm));

            return vms;
        }
    }
}