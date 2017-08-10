﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Core.TaskRunning.Compilers
{
    public interface ICompiler
    {
        string Compile(string sourceCodeFileName);
    }
}