using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class PointerPushCommand : ICommand
    {
        private readonly string index;

        public PointerPushCommand(string index)
        {
            this.index = index;
        }

        public IEnumerable<string> ToAssembly()
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