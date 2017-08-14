using ProgFrog.Interface.TaskRunning.Runners;
using System;
using System.IO;

namespace ProgFrog.Core.TaskRunning.Runners
{
    public class TempFileProvider : ITempFileProvider
    {
        private string _currentFilePath;

        public string CreateNewTempFile()
        {
            if(_currentFilePath != null)
            {
                throw new ApplicationException("Previous temp file isnt deleted yet");
            }

            _currentFilePath = Path.GetTempFileName();
            return _currentFilePath;
        }

        public void DeleteCurrentTempFile()
        {
            File.Delete(_currentFilePath);
            _currentFilePath = null;
        }
    }
}
