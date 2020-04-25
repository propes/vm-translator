using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class ConstantPushCommand : IConstantCommand
    {
        public IEnumerable<string> ToAssembly(string index)
        {
            return new []
            {
                $"@{index}",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
        }
    }
}