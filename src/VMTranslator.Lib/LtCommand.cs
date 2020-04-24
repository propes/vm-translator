using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class LtCommand : ICommand
    {
        private static int count = 0;

        public IEnumerable<string> ToAssembly()
        {
            return new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                $"@LT_{count}",
                "D;JLT",
                "@SP",
                "A=M-1",
                "M=0",
                $"@LT_END_{count}",
                "0;JMP",
                $"(LT_{count})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(LT_END_{count++})",
            };
        }
    }
}