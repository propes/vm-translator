using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public abstract class SegmentCommand : ICommand
    {
        private readonly Dictionary<string, string> segments = new Dictionary<string, string>
        {
            { "local", "LCL" },
            { "argument", "ARG" },
            { "this", "THIS" },
            { "that", "THAT" }
        };

        public SegmentCommand(string segment, string index)
        {
            this.Segment = segment;
            this.Index = index;
        }

        public string Segment { get; private set; }
        public string Index { get; private set; }

        protected string segmentCode => this.segments[this.Segment];

        public abstract IEnumerable<string> ToAssembly();
    }

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
                $"@{this.Index}",
                "D=A",
                $"@{this.segmentCode}",
                "A=M+D",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
        }
    }

    public class SegmentPopCommand : SegmentCommand
    {
        public SegmentPopCommand(string segment, string index) :
            base(segment, index)
        {
        }

        public override IEnumerable<string> ToAssembly()
        {
            return new []
            {
                "@SP",
                "M=M-1",
                "A=M",
                "D=M",
                
            };
        }
    }
}