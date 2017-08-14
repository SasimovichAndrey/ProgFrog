using NUnit.Framework;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Runners;
using System.Diagnostics;
using System.IO;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class OutputReaderTests : TestsBase
    {
        [Test]
        public void Test()
        {
            var proc = new Process();

            try
            {
                var fileName = Path.Combine(GetTestDataDirectoryPath(), "output12345.exe");
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;

                proc.Start();

                var reader = new StandardOutputStreamReader(new ProcessProxy(proc));
                var result = reader.Read();

                proc.Close();

                Assert.AreEqual("12345", result);
            }
            finally
            {
                proc.Close();
            }
        }
    }
}
