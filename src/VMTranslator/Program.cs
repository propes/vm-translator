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
            var filenameWithoutExt = Path.GetFileNameWithoutExtension(filename);

            fileReader = new FileReader();
            translator = new VMTranslator.Lib.VMTranslator(
                new TextCleaner(),
                new CommandTranslator(
                    new ArithmeticCommandTranslator(
                        new Counter(),
                        new Counter(),
                        new Counter()
                    ),
                    new StackOperationTranslator(
                        new CommandParser(),
                        new StackOperationTranslatorProvider(
                            new MemorySegmentPushCommandTranslator(),
                            new MemorySegmentPopCommandTranslator(),
                            new ConstantPushCommandTranslator(),
                            new StaticPushCommandTranslator(filenameWithoutExt),
                            new StaticPopCommandTranslator(filenameWithoutExt),
                            new PointerPushCommandTranslator(),
                            new PointerPopCommandTranslator(),
                            new TempPushCommandTranslator(),
                            new TempPopCommandTranslator()
                        )
                    )
                ));
            fileWriter = new FileWriter();

            var lines = fileReader.ReadFileToArray(filename);
            var translatedLines = translator.TranslateVMcodeToAssembly(lines);

            string outputFilename = Path.ChangeExtension(filename, "asm");
            fileWriter.WriteArrayToFile(outputFilename, translatedLines);
        }
    }
}
