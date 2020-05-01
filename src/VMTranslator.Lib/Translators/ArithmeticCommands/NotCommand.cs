using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class NotCommand : ICommand
    {
        public IEnumerable<string> ToAssembly()
        {
            return new string []
            {
                "// not",
                "@SP",
                "A=M-1",
                "M=!M",
                ""
            };
        }
    }
}