using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPopCommandTranslator : IStaticCommandTranslator
    {
        private readonly string variableName;

        public StaticPopCommandTranslator(string variableName)
        {
            this.variableName = variableName;
        }

        public IEnumerable<string> ToAssembly(string index)
        {
            return new[]
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{variableName}.{index}",
                "M=D"
            };
        }
    }
}