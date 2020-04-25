using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class EqCommand : ICommand
    {
        private readonly ICounter counter;

        public EqCommand(ICounter counter)
        {
            this.counter = counter;
        }

        public IEnumerable<string> ToAssembly()
        {
            var aseembly = new string []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@SP",
                "A=M-1",
                "D=M-D",
                $"@EQ_{counter.Count}",
                "D;JEQ",
                "@SP",
                "A=M-1",
                "M=0",
                $"@EQ_END_{counter.Count}",
                "0;JMP",
                $"(EQ_{counter.Count})",
                "@SP",
                "A=M-1",
                "M=-1",
                $"(EQ_END_{counter.Count})"
            };
            counter.Increment();

            return aseembly;
        }
    }
}