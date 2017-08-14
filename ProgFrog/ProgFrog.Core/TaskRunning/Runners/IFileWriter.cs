using System;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public interface IFileWriter
    {
        void Write(string str, string fileName);
    }
}
