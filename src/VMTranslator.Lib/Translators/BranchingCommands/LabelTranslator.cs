using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
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