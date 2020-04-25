using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IMemorySegmentCommand
    {
        IEnumerable<string> ToAssembly(string segment, string index);
    }
}