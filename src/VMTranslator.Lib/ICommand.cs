using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface ICommand
    {
        IEnumerable<string> ToAssembly();
    }
}