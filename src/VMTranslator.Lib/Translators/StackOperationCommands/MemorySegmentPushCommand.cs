using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class MemorySegmentPushCommandTranslator :
        MemorySegmentCommandTranslator,
        IMemorySegmentPushCommandTranslator
    {
        public override IEnumerable<string> ToAssembly(Command command)
        {
            var segmentCode = segmentCodes[command.Segment];

            return new []
            {
                $"@{segmentCode}",
                "D=M",
                $"@{command.Index}",
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