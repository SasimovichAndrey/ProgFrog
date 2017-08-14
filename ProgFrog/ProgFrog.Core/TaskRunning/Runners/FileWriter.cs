using ProgFrog.Interface.TaskRunning.Runners;
using System.IO;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class FileWriter : IFileWriter
    {

        public void Write(string str, string fileName)
        {
            using (var streamWriter = new StreamWriter(File.OpenWrite(fileName)))
            {
                streamWriter.WriteAsync(str);
            }
        }
    }
}
