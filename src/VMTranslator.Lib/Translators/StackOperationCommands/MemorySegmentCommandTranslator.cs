using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public abstract class MemorySegmentCommandTranslator : IStackOperationCommandTranslator
    {
        protected readonly Dictionary<string, string> segmentCodes = new Dictionary<string, string>
        {
            { "local", "LCL" },
            { "argument", "ARG" },
            { "this", "THIS" },
            { "that", "THAT" }
        };

        public abstract IEnumerable<string> ToAssembly(Command command);
    }
}