using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProgFrog.Interface.Data.Serialization;
using ProgFrog.Interface.Data;
using ProgFrog.Interface.Model;

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

        public async Task<ProgrammingTask> Create(ProgrammingTask task)
        {
            var taskId = Guid.NewGuid();
            task.Identifier = new GuidIdentifier(taskId);
            var serialized = _serializer.Serialize(task);

            var newFilePath = Path.Combine(_directoryPath, $"{taskId}.pt");
            using(var writer = new StreamWriter(File.Create(newFilePath)))
            {
                await writer.WriteAsync(serialized);
            }

            return task;
        }

        public async Task<IEnumerable<ProgrammingTask>> GetAll()
        {
            var fileNames = Directory.EnumerateFiles(_directoryPath, "*.pt");

            var tasks = new List<ProgrammingTask>();
            foreach(var fileName in fileNames)
            {
                var progTask = await GetTaskFromFile(fileName);

                tasks.Add(progTask);
            }

            return tasks;
        }

        private async Task<ProgrammingTask> GetTaskFromFile(string fileName)
        {
            ProgrammingTask progTask = null;
            using (var streamReader = new StreamReader(File.Open(fileName, FileMode.Open, FileAccess.Read)))
            {
                var fileContentsTask = streamReader.ReadToEndAsync();
                var pureFileName = Path.GetFileNameWithoutExtension(fileName);
                var id = new GuidIdentifier(new Guid(pureFileName));

                var fileContents = await fileContentsTask;
                progTask = _serializer.Deserialize(fileContents);
                progTask.Identifier = id;
            }

            return progTask;
        }

        public async Task<ProgrammingTask> GetById(IIdentifier identifier)
        {
            ProgrammingTask task = null;

            var fileName = Path.Combine(_directoryPath, identifier.StringPresentation + ".pt");
            if (File.Exists(fileName))
            {
                task = await GetTaskFromFile(fileName);
            }

            return task;
        }


    }
}
