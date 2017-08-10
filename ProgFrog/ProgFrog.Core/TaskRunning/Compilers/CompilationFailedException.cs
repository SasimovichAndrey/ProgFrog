using System;

namespace ProgFrog.Core.TaskRunning.Compilers.Exceptions
{
    public class CompilationFailedException : ApplicationException
    {
        public CompilationFailedException(string messagge) : base(messagge)
        {

        }
    }
}
