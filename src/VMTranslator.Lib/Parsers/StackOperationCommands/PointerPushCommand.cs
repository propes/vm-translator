using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class PointerPushCommand : IPointerCommand
    {
        public IEnumerable<string> ToAssembly(string index)
        {
            var segment = index == "0" ? "THIS" : "THAT";
            return new []
            {
                $"@{segment}",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
            };
        }
    }
}