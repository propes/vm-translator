using System;
using System.IO;
using VMTranslator.Lib;

namespace VMTranslator
{
    class Program
    {
        private static IFileReader fileReader;
        private static IVMTranslator translator;
        private static IFileWriter fileWriter;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify a vm file path");
                return;
            }

            var filename = args[0];

            fileReader = new FileReader();
            translator = new VMTranslator.Lib.VMTranslator(
                new TextCleaner(),
                new CommandParser(
                    new ArithmeticCommandParser(),
                    new StackOperationCommandParser(
                        Path.GetFileNameWithoutExtension(filename))
                ));
            fileWriter = new FileWriter();

            var lines = fileReader.ReadFileToArray(filename);
            var translatedLines = translator.TranslateVMcodeToAssembly(lines);

            string outputFilename = Path.ChangeExtension(filename, "asm");
            fileWriter.WriteArrayToFile(outputFilename, translatedLines);
        }
    }
}
