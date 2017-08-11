using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ProgFrog.Tests
{
    public class TestsBase
    {
        protected List<string> _filesToCleanup = new List<string>();

        protected string GetTestDataDirectoryPath()
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData"));
        }
    }
}
