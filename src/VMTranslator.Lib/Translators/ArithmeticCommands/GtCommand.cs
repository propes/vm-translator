using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class GtCommand : ICommand
    {
        private readonly ICounter counter;

        public GtCommand(ICounter counter)
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
                $"@GT_{counter.Count}",
                "D;JGT",
                "@SP",
                "A=M-1",
                "M=0",
                $"@GT_END_{counter.Count}",
                "0;JMP",
                $"(GT_{counter.Count})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(GT_END_{counter.Count})"
            };
            counter.Increment();

            return assembly;
        }
    }
}