using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class ConstantPushCommandTranslator : IConstantCommandTranslator
    {
        public IEnumerable<string> ToAssembly(Command command)
        {
            return new []
            {
                $"@{command.Index}",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
        }
    }
}