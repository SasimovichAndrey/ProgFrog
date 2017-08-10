using ProgFrog.Core.Model;
using System;

namespace ProgFrog.Core.TaskRunning
{
    public class RunnedTestResult
    {
        public ParamsAndResults ParamsAndResults { get; set; }
        public string Results { get; set; }
        public TimeSpan TimeElapsed { get;set; }
        public bool IsError { get; set; }
        public RunnedTaskErrorType ErrorType { get; set; }
    }
}
