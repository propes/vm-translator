using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class GtCommand : ICommand
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
                $"@GT_{count}",
                "D;JGT",
                "@SP",
                "A=M-1",
                "M=0",
                $"@GT_END_{count}",
                "0;JMP",
                $"(GT_{count})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(GT_END_{count++})"
            };
        }
    }
}