using ProgFrog.Core.TaskRunning.Runners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Core.TaskRunning
{
    public interface IInputWriter : IRunnerVisitor
    {
        void Write(string inp);
    }
}