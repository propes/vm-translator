using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPushCommand : IStaticCommand
    {
        public IEnumerable<string> ToAssembly(string variableName, string index)
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