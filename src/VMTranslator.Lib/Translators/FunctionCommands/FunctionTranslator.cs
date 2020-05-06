using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class FunctionTranslator : ICommandTranslator
    {
        private readonly string filename;

        public FunctionTranslator(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> ToAssembly(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 3)
            {
                throw new InvalidOperationException("Function command must be of the form 'function foo 2'");
            }

            if (!int.TryParse(parts[2], out int nVars))
            {
                throw new InvalidOperationException("nVars must be an integer");
            }

            var assembly = new List<string>();
            assembly.Add($"// {line}");
            assembly.Add($"({filename}.{parts[1]})");

            for (int i = 0; i < nVars; i++)
            {
                assembly.AddRange(new ConstantPushCommandTranslator()
                    .ToAssembly(new Command("push", "constant", "0")));
            }

            return assembly;
        }
    }
}