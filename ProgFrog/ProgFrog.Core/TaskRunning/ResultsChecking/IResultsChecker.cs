using System.Collections.Generic;

namespace ProgFrog.Core.TaskRunning.ResultsChecking
{
    public interface IResultsChecker
    {
        CheckResult Check(IEnumerable<RunnedTestResult> results);
    }
}
