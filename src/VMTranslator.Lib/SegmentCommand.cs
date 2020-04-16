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
}