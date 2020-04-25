using System.IO;
using VMTranslator.Lib;

namespace VMTranslator
{
    public class FileReader : IFileReader
    {
        public string[] ReadFileToArray(string filename)
        {
            return File.ReadAllLines(filename);
        }
    }
}