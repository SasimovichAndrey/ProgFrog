using System;
using ProgFrog.Core.TaskRunning;
using System.Collections.Generic;

namespace ProgFrog.WpfApp.Infrastructure
{
    public class TaskRunnerProvider : ITaskRunnerProvider
    {
        private IDictionary<ProgrammingLanguageEnum, IProgTaskRunner> _langToRunnerMappings = new Dictionary<ProgrammingLanguageEnum, IProgTaskRunner>();

        public void RegisterRunner(IProgTaskRunner runner, ProgrammingLanguageEnum lang)
        {
            _langToRunnerMappings[lang] = runner;
        }

        public IProgTaskRunner GetRunner(ProgrammingLanguageEnum lang)
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

        public IEnumerable<ProgrammingLanguageEnum> GetAvailableLanguages()
        {
            return _langToRunnerMappings.Keys;
        }
    }
}
