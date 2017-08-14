using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.ResultsChecking;
using System;
using System.Collections.Generic;

namespace ProgFrog.Core.TaskRunning.ResultsChecking
{
    public class ResultsChecker : IResultsChecker
    {
        public CheckResult Check(IEnumerable<RunnedTestResult> results)
        {
            var result = new CheckResult
            {
                ErrorType = null,
                IsSuccessfull = true
            };

            foreach(var res in results)
            {
                if (res.IsError)
                {
                    result.IsSuccessfull = false;
                    switch(res.ErrorType)
                    {
                        case RunnedTaskErrorType.RuntimeException:
                            result.ErrorType = ResultFailureType.RuntimeException;
                            break;
                        default:
                            throw new ApplicationException("Unknown RunnedTaskErrorType");
                    }
                }
                else if(res.ParamsAndResults.Results != res.Results)
                {
                    result.IsSuccessfull = false;
                    result.ErrorType = ResultFailureType.WrongResults;
                }    
            }

            return result;
        }
    }
}
