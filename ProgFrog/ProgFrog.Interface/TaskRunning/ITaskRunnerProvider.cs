using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning;
using System.Collections.Generic;

namespace ProgFrog.Interface.TaskRunning
{
    public interface ITaskRunnerProvider
    {
        IProgTaskRunner GetRunner(ProgrammingLanguage lang);
        IEnumerable<ProgrammingLanguage> GetAvailableLanguages();
    }
}
