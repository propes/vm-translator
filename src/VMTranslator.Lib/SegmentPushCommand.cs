using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class SegmentPushCommand : SegmentCommand
    {
        public SegmentPushCommand(string segment, string index) :
            base(segment, index)
        {
        }

        public override IEnumerable<string> ToAssembly()
        {
            return new []
            {
                $"@{this.segmentCode}",
                "D=M",
                $"@{this.Index}",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
        }
    }
}