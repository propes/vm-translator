using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class IfGotoTranslator : ICommandTranslator
    {
        public IEnumerable<string> ToAssembly(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 2)
            {
                throw new InvalidOperationException("if-goto command must be of the form 'if-goto foo'");
            }

            return new []
            {
                $"// {line}",
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{parts[1]}",
                "D;JNE",
                ""
            };
        }
    }
}