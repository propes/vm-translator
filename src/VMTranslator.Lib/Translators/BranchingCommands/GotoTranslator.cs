using System;
using System.Collections.Generic;
using VMTranslator.Lib;

namespace VMTranslator
{
    public class GotoTranslator : ICommandTranslator
    {
        public IEnumerable<string> ToAssembly(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 2)
            {
                throw new InvalidOperationException("goto command must be of the form 'goto foo'");
            }

            return new []
            {
                $"// {line}",
                $"@{parts[1]}",
                "0;JMP",
                ""
            };
        }
    }
}