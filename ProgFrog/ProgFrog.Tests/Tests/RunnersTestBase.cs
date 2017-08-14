using Moq;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Runners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _fileWriterMock = new Mock<IFileWriter>();
            _processFactoryMock = new Mock<IProcessFactory>();
            _inputWriterMock = new Mock<IInputWriter>();
            _outReaderMock = new Mock<IOutputReader>();
            _tempFileProviderMock = new Mock<ITempFileProvider>();
        }
    }
}
