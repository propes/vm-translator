using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface IConstantCommand
    {
        IEnumerable<string> ToAssembly(string index);
    }
}