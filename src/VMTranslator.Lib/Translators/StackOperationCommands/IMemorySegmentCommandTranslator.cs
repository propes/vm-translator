using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IMemorySegmentCommandTranslator
    {
        IEnumerable<string> ToAssembly(string segment, string index);
    }
}