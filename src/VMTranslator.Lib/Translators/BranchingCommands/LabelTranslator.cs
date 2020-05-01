using System;
using System.Collections.Generic;
using System.Text;

namespace VMTranslator.Lib
{
    public class LabelTranslator : ICommandTranslator
    {
        private readonly string filename;
        private readonly IFunctionState functionState;

        public LabelTranslator(string filename, IFunctionState functionState)
        {
            this.filename = filename;
            this.functionState = functionState;
        }

        public IEnumerable<string> ToAssembly(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Label command must be of the form 'label foo'");
            }

            var sb = new StringBuilder();
            sb.Append("(");
            sb.Append($"{filename}.");
            if (!string.IsNullOrEmpty(functionState.CurrentFunction))
            {
                sb.Append($"{functionState.CurrentFunction}$");
            }
            sb.Append(parts[1]);
            sb.Append(")");

            return new [] { sb.ToString() };
        }
    }
}