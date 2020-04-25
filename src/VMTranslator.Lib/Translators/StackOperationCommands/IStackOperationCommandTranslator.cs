using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IStackOperationCommandTranslator
    {
        IEnumerable<string> ToAssembly(Command command);
    }
}