using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class ProgTaskRunResult
    {
        private TaskRunErrorType? _errorType;

        public IList<RunnedTestResult> Results { get; set; } = new List<RunnedTestResult>();
        public TaskRunErrorType? ErrorType { get; private set; }

        public bool IsRunError { get; private set; }

        public void SetRunError(TaskRunErrorType errType)
        {
            ErrorType = errType;
            IsRunError = true;
        }
    }
}
