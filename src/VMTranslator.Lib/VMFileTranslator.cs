using System;
using System.Collections.Generic;
using System.IO;

namespace VMTranslator.Lib
{
    public class VMFileTranslator
    {
        private readonly IVMTranslator vmTranslator;

        public VMFileTranslator(IVMTranslator vmTranslator)
        {
            this.vmTranslator = vmTranslator;
        }

        public void TranslateFileToStream(string inputFilename, StreamWriter sw)
        {
            var vmLines = File.ReadAllLines(inputFilename);

            var assemblyLines = vmTranslator.TranslateVMcodeToAssembly(vmLines);

            foreach (var assemblyLine in assemblyLines)
            {
                sw.WriteLine(assemblyLine);
            }
        }
    }
}