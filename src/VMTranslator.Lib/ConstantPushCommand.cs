using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class ConstantPushCommand : ICommand
    {
        public string Index { get; private set; }

        public ConstantPushCommand(string index)
        {
            this.Index = index;
        }

        public IEnumerable<string> ToAssembly()
        {
            return new []
            {
                $"@{this.Index}",
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