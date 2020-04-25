using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IPointerCommand
    {
        IEnumerable<string> ToAssembly(string index);
    }
}