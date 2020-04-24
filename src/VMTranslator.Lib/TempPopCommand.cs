using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class TempPopCommand : ICommand
    {
        private readonly string index;

        public TempPopCommand(string index)
        {
            this.index = index;
        }

        public IEnumerable<string> ToAssembly()
        {
            var lines = new List<string>();
            lines.AddRange(new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@R5"
            });
            for (int i = 0; i < int.Parse(this.index); i++)
            {
                lines.Add("A=A+1");
            }

            lines.Add("M=D");

            return lines;
        }
    }
}