using System;

namespace ProgFrog.Core.TaskRunning
{
    public class RunnedTaskResult
    {
        public string Results { get; set; }
        public TimeSpan TimeElapsed { get;set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
