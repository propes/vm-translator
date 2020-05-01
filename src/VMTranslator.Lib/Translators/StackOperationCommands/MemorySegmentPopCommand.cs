using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class MemorySegmentPopCommandTranslator :
        MemorySegmentCommandTranslator,
        IMemorySegmentPopCommandTranslator
    {
        public override IEnumerable<string> ToAssembly(Command command)
        {
            var segmentCode = segmentCodes[command.Segment];
            var lines = new List<string>();
            lines.AddRange(new []
            {
                $"// {command.ToString()}",
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{segmentCode}",
                "A=M"
            });
            for (int i = 0; i < int.Parse(command.Index); i++)
            {
                lines.Add("A=A+1");
            }

            lines.Add("M=D");
            lines.Add("");

            return lines;
        }
    }
}