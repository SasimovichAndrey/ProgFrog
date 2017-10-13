using Moq;
using NUnit.Framework;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Runners;

namespace ProgFrog.Tests.Tests
{
    public class RunnersTestBase : CompilerTestBase
    {
        protected Mock<IFileWriter> _fileWriterMock;
        protected Mock<IProcessFactory> _processFactoryMock;
        protected Mock<IInputWriter> _inputWriterMock;
        protected Mock<IOutputReader> _outReaderMock;
        protected Mock<ITempFileProvider> _tempFileProviderMock;

        public RunnersTestBase()
        {
            
        }

        [SetUp]
        public void Setup()
        {
            _fileWriterMock = new Mock<IFileWriter>();
            _processFactoryMock = new Mock<IProcessFactory>();
            _inputWriterMock = new Mock<IInputWriter>();
            _outReaderMock = new Mock<IOutputReader>();
            _tempFileProviderMock = new Mock<ITempFileProvider>();
        }
    }
}
