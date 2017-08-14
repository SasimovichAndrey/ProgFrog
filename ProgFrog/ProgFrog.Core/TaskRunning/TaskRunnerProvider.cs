using System;
using System.Collections.Generic;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.Model;

namespace ProgFrog.Core.TaskRunning
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
