using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class PointerPopCommandTranslator : IPointerPopCommandTranslator
    {
        public IEnumerable<string> ToAssembly(Command command)
        {
            var segment = command.Index == "0" ? "THIS" : "THAT";
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