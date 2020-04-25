using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IPointerCommandTranslator
    {
        IEnumerable<string> ToAssembly(Command command);
    }
}