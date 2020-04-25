using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPopCommand : IStaticCommand
    {
        public IEnumerable<string> ToAssembly(string variableName, string index)
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