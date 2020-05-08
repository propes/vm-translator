using System;
using System.Collections.Generic;
using System.IO;
using VMTranslator.Lib;

namespace VMTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify a vm file or folder");
                return;
            }

            IEnumerable<string> inputFilenames;
            if (File.Exists(args[0]))
            {
                inputFilenames = new[] { args[0] };
            }
            else if (Directory.Exists(args[0]))
            {
                inputFilenames = Directory.EnumerateFiles(args[0], "*.vm");
            }
            else
            {
                Console.WriteLine("Please specify a valid vm file or folder");
                return;
            }

            var outputFilename = Path.ChangeExtension(args[0], "asm");
            using (var sw = File.AppendText(outputFilename))
            {
                AddBootstrappingCode(sw);

                foreach (var inputFilename in inputFilenames)
                {
                    var filenameWithoutExt = Path.GetFileNameWithoutExtension(inputFilename);

                    new VMFileTranslator(CreateTranslator(filenameWithoutExt))
                        .TranslateFileToStream(inputFilename, sw);
                }
            }
        }

        private static void AddBootstrappingCode(StreamWriter sw)
        {
            sw.WriteLine("SP=256");
            sw.WriteLine("Call Sys.init");
        }

        private static IVMTranslator CreateTranslator(string filenameWithoutExt)
        {
            return new VMTranslator.Lib.VMTranslator(
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
                    ),
                    new LabelTranslator(filenameWithoutExt, new FunctionState()),
                    new GotoTranslator(filenameWithoutExt),
                    new IfGotoTranslator(filenameWithoutExt),
                    new FunctionTranslator(),
                    new ReturnTranslator(),
                    new CallFunctionTranslator(new FunctionCallCounter())
                ));
        }
    }
}
