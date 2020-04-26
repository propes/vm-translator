using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class PointerPushCommandTranslator : IPointerPushCommandTranslator
    {
        public IEnumerable<string> ToAssembly(Command command)
        {
            var segment = command.Index == "0" ? "THIS" : "THAT";
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