﻿using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IConstantCommandTranslator
    {
        IEnumerable<string> ToAssembly(string index);
    }
}