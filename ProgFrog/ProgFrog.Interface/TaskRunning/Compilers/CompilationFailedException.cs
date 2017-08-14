using System;

namespace ProgFrog.Interface.TaskRunning.Compilers.Exceptions
{
    public class CompilationFailedException : ApplicationException
    {
        public CompilationFailedException(string messagge) : base(messagge)
        {

        }
    }
}
