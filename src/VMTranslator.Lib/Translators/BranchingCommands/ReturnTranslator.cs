using System.Collections.Generic;
using VMTranslator.Lib;

namespace VMTranslator
{
    public class ReturnTranslator : ICommandTranslator
    {
        public IEnumerable<string> ToAssembly(string line)
        {
            return new []
            {
                "// return",
                "@LCL",
                "D=M",
                "@endFrame",
                "M=D",
                "@5",
                "A=D-A",
                "D=M",
                "@retAddr",
                "M=D",
                "@SP",
                "AM=M-1",
                "D=M",
                "@ARG",
                "A=M",
                "M=D",
                "D=A+1",
                "@SP",
                "M=D",
                "@endFrame",
                "A=M-1",
                "D=M",
                "@THAT",
                "M=D",
                "@endFrame",
                "A=M-1",
                "A=A-1",
                "D=M",
                "@THIS",
                "M=D",
                "@endFrame",
                "A=M-1",
                "A=A-1",
                "A=A-1",
                "D=M",
                "@ARG",
                "M=D",
                "@endFrame",
                "A=M-1",
                "A=A-1",
                "A=A-1",
                "A=A-1",
                "D=M",
                "@LCL",
                "M=D",
                "@retAddr",
                "A=M",
                "0;JMP",
                ""
            };
        }
    }
}