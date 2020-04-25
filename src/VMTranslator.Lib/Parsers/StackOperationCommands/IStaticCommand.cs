using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IStaticCommand
    {
        IEnumerable<string> ToAssembly(string index);
    }
}