using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class TempPushCommand : ICommand
    {
        private readonly string index;

        public TempPushCommand(string index)
        {
            this.index = index;
        }

        public IEnumerable<string> ToAssembly()
        {
            return new []
            {
                "@R5",
                "D=A",
                $"@{index}",
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