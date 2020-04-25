using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class LtCommand : ICommand
    {
        private readonly ICounter counter;

        public LtCommand(ICounter counter)
        {
            this.counter = counter;
        }

        public IEnumerable<string> ToAssembly()
        {
            var assembly = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                $"@LT_{counter.Count}",
                "D;JLT",
                "@SP",
                "A=M-1",
                "M=0",
                $"@LT_END_{counter.Count}",
                "0;JMP",
                $"(LT_{counter.Count})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(LT_END_{counter.Count})",
            };
            counter.Increment();

            return assembly;
        }
    }
}