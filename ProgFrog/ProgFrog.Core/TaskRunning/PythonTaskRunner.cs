using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgFrog.Core.Model;

namespace ProgFrog.Core.TaskRunning
{
    public class PythonTaskRunner : IProgTaskRunner
    {
        private string _pyInterpreterPath;

        public PythonTaskRunner(string interpreterPath)
        {

        }

        public RunnedTaskResult Run(ProgrammingTask task, string userCode)
        {
            throw new NotImplementedException();
        }
    }
}
