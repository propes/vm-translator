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
            if (args.Length < 1)
            {
                Console.WriteLine("Please specify a vm file or folder");
                return;
            }

            var path = args[0];

            IEnumerable<string> inputFilenames;
            string outputFilename;
            var isBootstrappingRequired = false;
            if (File.Exists(path))
            {
                inputFilenames = new[] { path };
                outputFilename = Path.ChangeExtension(path, "asm");
            }
            else if (Directory.Exists(path))
            {
                inputFilenames = Directory.EnumerateFiles(path, "*.vm");
                outputFilename = path.TrimEnd('/') + ".asm";
                isBootstrappingRequired = true;
            }
            else
            {
                Console.WriteLine("Please specify a valid vm file or folder");
                return;
            }

            var eqCommandCounter = new Counter();
            var gtCommandCounter = new Counter();
            var ltCommandCounter = new Counter();
            var functionCallCounter = new FunctionCallCounter();

            using (var sw = File.CreateText(outputFilename))
            {
                if (isBootstrappingRequired)
                {
                    AddBootstrappingCode(sw);
                }

                foreach (var inputFilename in inputFilenames)
                {
                    var filenameWithoutExt = Path.GetFileNameWithoutExtension(inputFilename);

                    new VMFileTranslator(CreateTranslator(
                            eqCommandCounter,
                            gtCommandCounter,
                            ltCommandCounter,
                            functionCallCounter,
                            filenameWithoutExt
                        ))
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

        private static IVMTranslator CreateTranslator(
            ICounter eqCommandCounter,
            ICounter gtCommandCounter,
            ICounter ltCommandCounter,
            IFunctionCallCounter functionCallCounter,
            string filenameWithoutExt)
        {
            return new VMTranslator.Lib.VMTranslator(
                new TextCleaner(),
                new CommandTranslator(
                    new ArithmeticCommandTranslator(
                        eqCommandCounter,
                        gtCommandCounter,
                        ltCommandCounter
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
                    new CallFunctionTranslator(functionCallCounter)
                ));
        }
    }
}
