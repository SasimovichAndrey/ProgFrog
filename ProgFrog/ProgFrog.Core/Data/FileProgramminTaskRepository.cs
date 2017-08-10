using System;
using System.Collections.Generic;
using ProgFrog.Core.Model;
using System.IO;
using System.Threading.Tasks;
using ProgFrog.Core.Data.Serialization;

namespace ProgFrog.Core.Data
{
    public class FileProgramminTaskRepository : IProgrammingTaskRepository
    {
        private string _directoryPath;
        private IModelSerializer<ProgrammingTask> _serializer;

        public FileProgramminTaskRepository(IModelSerializer<ProgrammingTask> serializer, string directoryPath)
        {
            _serializer = serializer;
            _directoryPath = directoryPath;
        }

        public async Task<IEnumerable<ProgrammingTask>> GetAll()
        {
            var fileNames = Directory.EnumerateFiles(_directoryPath, "*.pt");

            var tasks = new List<ProgrammingTask>();
            foreach(var fileName in fileNames)
            {
                using (var streamReader = new StreamReader(File.Open(fileName, FileMode.Open, FileAccess.Read)))
                {
                    var fileContentsTask = streamReader.ReadToEndAsync();
                    var pureFileName = Path.GetFileNameWithoutExtension(fileName);
                    var id = new GuidIdentifier(new Guid(pureFileName));

                    var fileContents = await fileContentsTask;
                    var progTask = _serializer.Deserialize(fileContents);
                    progTask.Identifier = id;

                    tasks.Add(progTask);
                }
            }

            return tasks;
        }
    }
}
