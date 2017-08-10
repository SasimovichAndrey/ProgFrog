using System.IO;
using System.Reflection;

namespace ProgFrog.Tests
{
    public class TestsBase
    {
        protected string GetTestDataDirectoryPath()
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData"));
        }
    }
}
