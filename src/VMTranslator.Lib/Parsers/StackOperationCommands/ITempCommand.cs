using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface ITempCommand
    {
        IEnumerable<string> ToAssembly(string index);
    }
}