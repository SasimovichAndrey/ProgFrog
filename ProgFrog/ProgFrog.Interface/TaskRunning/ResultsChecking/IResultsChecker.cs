using System.Collections.Generic;

namespace ProgFrog.Interface.TaskRunning.ResultsChecking
{
    public interface IResultsChecker
    {
        CheckResult Check(IEnumerable<RunnedTestResult> results);
    }
}
