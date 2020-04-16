using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class SegmentPopCommand : SegmentCommand
    {
        public SegmentPopCommand(string segment, string index) :
            base(segment, index)
        {
        }

        public override IEnumerable<string> ToAssembly()
        {
            var lines = new List<string>();
            lines.AddRange(new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{this.segmentCode}",
                "A=M"
            });
            for (int i = 0; i < int.Parse(this.Index); i++)
            {
                lines.Add("A=A+1");
            }

            lines.Add("M=D");

            return lines;
        }
    }
}