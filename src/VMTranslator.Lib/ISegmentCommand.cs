using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface ISegmentCommand
    {
        IEnumerable<string> ToAssembly(string segment, string index);
    }
}