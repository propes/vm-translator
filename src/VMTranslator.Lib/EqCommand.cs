using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class EqCommand : ICommand
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
                $"@EQ_{count}",
                "D;JEQ",
                "@SP",
                "A=M-1",
                "M=0",
                $"@EQ_END_{count}",
                "0;JMP",
                $"(EQ_{count})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(EQ_END_{count++})"
            };
        }
    }
}