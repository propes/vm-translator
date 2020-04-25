using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class TempPushCommand : ITempCommand
    {
        public IEnumerable<string> ToAssembly(string index)
        {
            return new []
            {
                "@R5",
                "D=A",
                $"@{index}",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
        }
    }
}