using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class MemorySegmentPopCommandTranslator : MemorySegmentCommandTranslator
    {
        public override IEnumerable<string> ToAssembly(string segment, string index)
        {
            var segmentCode = segmentCodes[segment];
            var lines = new List<string>();
            lines.AddRange(new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{segmentCode}",
                "A=M"
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