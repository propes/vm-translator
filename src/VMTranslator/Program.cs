using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VMTranslator.Lib;

namespace VMTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please specify a vm file or folder");
                return;
            }

            var path = args[0];

            IEnumerable<string> inputFilenames;
            string outputFilename;
            if (File.Exists(path))
            {
                inputFilenames = new[] { path };
                outputFilename = Path.ChangeExtension(path, "asm");
            }
            else if (Directory.Exists(path))
            {
                inputFilenames = Directory.EnumerateFiles(path, "*.vm");
                outputFilename = path.TrimEnd('/') + ".asm";
            }
            else
            {
                Console.WriteLine("Please specify a valid vm file or folder");
                return;
            }

            using (var sw = File.CreateText(outputFilename))
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
            var bootstrapLines = new BootstrapCode().ToAssembly();
            foreach(var line in bootstrapLines)
            {
                sw.WriteLine(line);
            }
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
