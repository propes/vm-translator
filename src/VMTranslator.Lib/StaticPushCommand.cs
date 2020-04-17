using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPushCommand : ICommand
    {
        private readonly string variable;
        private readonly string index;

        public StaticPushCommand(string variable, string index)
        {
            this.variable = variable;
            this.index = index;
        }

        public IEnumerable<string> ToAssembly()
        {
            return new[]
            {
                $"@{variable}.{index}",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
            };
        }
    }
}