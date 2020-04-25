using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPushCommand : IStaticCommand
    {
        private readonly string variableName;

        public StaticPushCommand(string variableName)
        {
            this.variableName = variableName;
        }

        public IEnumerable<string> ToAssembly(string index)
        {
            return new[]
            {
                $"@{variableName}.{index}",
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