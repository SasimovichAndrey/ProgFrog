using NUnit.Framework;
using ProgFrog.Core.TaskRunning.Compilers;
using System.IO;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class CSharpCompilerTests : CompilerTestBase
    {
        private string _execFileName;

        [Test]
        public void Test()
        {;
            var compiler = new CSharpCompiler(_cSharpCompPath);
            var fileName = Path.Combine(GetTestDataDirectoryPath(), "HelloWorld.cs");
            _execFileName = compiler.Compile(fileName);

            Assert.True(File.Exists(_execFileName));
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_execFileName))
            {
                File.Delete(_execFileName);
            }
        }
    }
}
