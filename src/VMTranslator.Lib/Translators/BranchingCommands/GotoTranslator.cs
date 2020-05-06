using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class GotoTranslator : ICommandTranslator
    {
        private readonly string filename;

        public GotoTranslator(string filename)
        {
            this.filename = filename;
        }

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
                $"@{filename}.{parts[1]}",
                "0;JMP",
                ""
            };
        }
    }
}