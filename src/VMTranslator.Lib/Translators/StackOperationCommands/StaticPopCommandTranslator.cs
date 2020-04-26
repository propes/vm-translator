using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPopCommandTranslator : IStaticPopCommandTranslator
    {
        private readonly string variableName;

        public StaticPopCommandTranslator(string variableName)
        {
            this.variableName = variableName;
        }

        public IEnumerable<string> ToAssembly(Command command)
        {
            return new[]
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{variableName}.{command.Index}",
                "M=D"
            };
        }
    }
}