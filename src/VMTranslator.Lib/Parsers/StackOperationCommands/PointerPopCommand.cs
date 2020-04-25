using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class PointerPopCommand : IPointerCommand
    {
        public IEnumerable<string> ToAssembly(string index)
        {
            var segment = index == "0" ? "THIS" : "THAT";
            return new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{segment}",
                "M=D",
            };
        }
    }
}