using System;
using System.Collections.Generic;
using VMTranslator.Lib;

namespace VMTranslator
{
    public class LabelTranslator : ICommandTranslator
    {
        public IEnumerable<string> ToAssembly(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Label command must be of the form 'label foo'");
            }

            return new [] { $"({parts[1]})" };
        }
    }
}