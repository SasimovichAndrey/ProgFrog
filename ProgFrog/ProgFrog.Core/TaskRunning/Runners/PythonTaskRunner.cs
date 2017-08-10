using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning.Runners;

namespace ProgFrog.Core.TaskRunning
{
    public class PythonTaskRunner : IProgTaskRunner
    {
        private string _pyInterpreterPath;

        public PythonTaskRunner(string interpreterPath)
        {

        }

        Task<ProgTaskRunResult> IProgTaskRunner.Run(ProgrammingTask task, string userCode)
        {
            throw new NotImplementedException();
        }
    }
}
