using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class MemorySegmentPushCommandTranslator : MemorySegmentCommandTranslator
    {
        public override IEnumerable<string> ToAssembly(string segment, string index)
        {
            var segmentCode = segmentCodes[segment];

            return new []
            {
                $"@{segmentCode}",
                "D=M",
                $"@{index}",
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