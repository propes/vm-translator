using System;
using VMTranslator.Lib;

namespace VMTranslator
{
    class Program
    {
        private static readonly IFileReader fileReader;
        private static readonly IVMTranslator translator;
        private static readonly IFileWriter fileWriter;

        static void Main(string[] args)
        {
            var filename = args[0];
            var lines = fileReader.ReadFileToArray(filename);
            var translatedLines = translator.TranslateVMcodeToAssembly(lines);
            fileWriter.WriteArrayToFile(translatedLines);
        }
    }
}
