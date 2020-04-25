using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface ICommandTranslator
    {
        IEnumerable<string> ToAssembly(string line);
    }
}