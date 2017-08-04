using System;

namespace ProgFrog.Core.TaskRunning
{
    public class RunnedTaskResult
    {
        public bool IsCorrect { get; set; }
        public TimeSpan TimeElapsed { get;set; }
    }
}
