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
                "// endFrame = LCL",
                "@LCL",
                "D=M",
                "@endFrame",
                "M=D",
                "// retAddr = *(endFrame-5)",
                "@5",
                "A=D-A",
                "D=M",
                "@retAddr",
                "M=D",
                "// *ARG = pop()",
                "@SP",
                "AM=M-1",
                "D=M",
                "@ARG",
                "A=M",
                "M=D",
                "// SP = ARG+1",
                "D=A+1",
                "@SP",
                "M=D",
                "// THAT = *(endFrame-1)",
                "@endFrame",
                "A=M-1",
                "D=M",
                "@THAT",
                "M=D",
                "// THIS = *(endFrame-2)",
                "@endFrame",
                "A=M-1",
                "A=A-1",
                "D=M",
                "@THIS",
                "M=D",
                "// ARG = *(endFrame-3)",
                "@endFrame",
                "A=M-1",
                "A=A-1",
                "A=A-1",
                "D=M",
                "@ARG",
                "M=D",
                "// LCL = *(endFrame-4)",
                "@endFrame",
                "A=M-1",
                "A=A-1",
                "A=A-1",
                "A=A-1",
                "D=M",
                "@LCL",
                "M=D",
                "// goto retAddr",
                "@retAddr",
                "A=M",
                "0;JMP",
                ""
            };
        }
    }
}