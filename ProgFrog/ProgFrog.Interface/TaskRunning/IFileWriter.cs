using System;

namespace ProgFrog.Interface.TaskRunning.Runners
{
    public interface IFileWriter
    {
        void Write(string str, string fileName);
    }
}
