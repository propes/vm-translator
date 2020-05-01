using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class NegCommand : ICommand
    {
        public IEnumerable<string> ToAssembly()
        {
            return new string []
            {
                "// neg",
                "@SP",
                "A=M-1",
                "M=-M",
                ""
            };
        }
    }
}