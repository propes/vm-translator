using System.IO;
using VMTranslator.Lib;

namespace VMTranslator
{
    public class FileWriter : IFileWriter
    {
        public void WriteArrayToFile(string filename, string[] translatedLines)
        {
            File.WriteAllLines(filename, translatedLines);
        }
    }
}