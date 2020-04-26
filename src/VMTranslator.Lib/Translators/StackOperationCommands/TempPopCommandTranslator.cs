using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class TempPopCommandTranslator : ITempPopCommandTranslator
    {
        public IEnumerable<string> ToAssembly(Command command)
        {
            var lines = new List<string>();
            lines.AddRange(new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@R5"
            });
            for (int i = 0; i < int.Parse(command.Index); i++)
            {
                lines.Add("A=A+1");
            }

            lines.Add("M=D");

            return lines;
        }
    }
}