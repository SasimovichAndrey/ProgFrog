using ProgFrog.Core.TaskRunning;
using System.Collections.Generic;

namespace ProgFrog.WpfApp.Infrastructure
{
    public interface ITaskRunnerProvider
    {
        IProgTaskRunner GetRunner(ProgrammingLanguageEnum lang);
        IEnumerable<ProgrammingLanguageEnum> GetAvailableLanguages();
    }
}
