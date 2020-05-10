using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class BootstrapCode
    {
        public IEnumerable<string> ToAssembly()
        {
            var assemblyLines = new List<string>();
            assemblyLines.AddRange(new []
            {
                "// SP=256",
                "@256",
                "D=A",
                "@SP",
                "M=D",
                ""
            });
            assemblyLines
                .AddRange(new CallFunctionTranslator(new FunctionCallCounter())
                    .ToAssembly("call Sys.init 0"));

            return assemblyLines;
        }
    }
}