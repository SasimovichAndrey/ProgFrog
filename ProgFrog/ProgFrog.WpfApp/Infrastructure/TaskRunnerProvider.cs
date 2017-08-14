using System;
using ProgFrog.Core.TaskRunning;
using System.Collections.Generic;

namespace ProgFrog.WpfApp.Infrastructure
{
    public class TaskRunnerProvider : ITaskRunnerProvider
    {
        private IDictionary<ProgrammingLanguage, IProgTaskRunner> _langToRunnerMappings = new Dictionary<ProgrammingLanguage, IProgTaskRunner>();

        public void RegisterRunner(IProgTaskRunner runner, ProgrammingLanguage lang)
        {
            _langToRunnerMappings[lang] = runner;
        }

        public IProgTaskRunner GetRunner(ProgrammingLanguage lang)
        {
            if (_langToRunnerMappings.ContainsKey(lang))
            {
                return _langToRunnerMappings[lang];
            }
            else
            {
                throw new ApplicationException("Unknow language type");
            }
        }

        public IEnumerable<ProgrammingLanguage> GetAvailableLanguages()
        {
            return _langToRunnerMappings.Keys;
        }
    }
}
