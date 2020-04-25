using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public interface ICommandParser
    {
        IEnumerable<string> Parse(string line);
    }
}