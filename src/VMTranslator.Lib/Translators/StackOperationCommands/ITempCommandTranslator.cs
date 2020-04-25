using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface ITempCommandTranslator
    {
        IEnumerable<string> ToAssembly(Command command);
    }
}