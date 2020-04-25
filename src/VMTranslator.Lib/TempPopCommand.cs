using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class TempPopCommand : ITempCommand
    {
        public IEnumerable<string> ToAssembly(string index)
        {
            var lines = new List<string>();
            lines.AddRange(new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@R5"
            });
            for (int i = 0; i < int.Parse(index); i++)
            {
                lines.Add("A=A+1");
            }

            lines.Add("M=D");

            return lines;
        }
    }
}