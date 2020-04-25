using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class OrCommand : ICommand
    {
        public IEnumerable<string> ToAssembly()
        {
            return new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "M=D|M"
            };
        }
    }
}