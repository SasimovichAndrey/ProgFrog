using NUnit.Framework;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Runners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class InputWriterTests : TestsBase
    {
        [Test]
        public void Test()
        {
            var fileName = Path.Combine(GetTestDataDirectoryPath(), "outIn.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            var proc = Process.Start(startInfo);
            try
            {
                var inputWriter = new StandardInputStreamWriter(new ProcessProxy(proc));
                var input = "123";
                inputWriter.Write(input);
                inputWriter.Write(Environment.NewLine);

                var outp = proc.StandardOutput.ReadToEnd();

                Assert.AreEqual(input + Environment.NewLine, outp);
            }
            finally
            {
                proc.Close();
            }
        }
    }
}
