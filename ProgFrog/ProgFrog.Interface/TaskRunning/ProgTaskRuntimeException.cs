﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFrog.Interface.TaskRunning.Runners
{
    public class ProgTaskRuntimeException : ApplicationException
    {
        public ProgTaskRuntimeException(string message) : base(message)
        {

        }
    }
}
