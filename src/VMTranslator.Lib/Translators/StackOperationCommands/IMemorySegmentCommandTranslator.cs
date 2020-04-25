using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IMemorySegmentCommandTranslator
    {
        IEnumerable<string> ToAssembly(Command command);
    }
}