using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class TempPushCommandTranslator : ITempPushCommandTranslator
    {
        public IEnumerable<string> ToAssembly(Command command)
        {
            return new []
            {
                "@R5",
                "D=A",
                $"@{command.Index}",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
        }
    }
}