using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IStaticCommandTranslator
    {
        IEnumerable<string> ToAssembly(Command command);
    }
}