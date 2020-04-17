using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPopCommand : ICommand
    {
        private readonly string variableName;
        private readonly string index;

        public StaticPopCommand(string variableName, string index)
        {
            this.variableName = variableName;
            this.index = index;
        }

        public IEnumerable<string> ToAssembly()
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