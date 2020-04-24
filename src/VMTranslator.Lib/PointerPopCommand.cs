using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class PointerPopCommand : ICommand
    {
        private readonly string index;

        public PointerPopCommand(string index)
        {
            this.index = index;
        }

        public IEnumerable<string> ToAssembly()
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