using System;
using System.Collections.Generic;
using VMTranslator.Lib;

namespace VMTranslator
{
    public class CallFunctionTranslator : ICommandTranslator
    {
        private readonly IFunctionCallCounter callCounter;

        public CallFunctionTranslator(IFunctionCallCounter callCounter)
        {
            this.callCounter = callCounter;
        }

        public IEnumerable<string> ToAssembly(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 3)
            {
                throw new InvalidOperationException("Call command must be of the form 'call foo 2'");
            }

            var functionName = parts[1];
            var nArgs = parts[2];
            var callCount = callCounter.GetCount(functionName);

            var assembly = new []
            {
                $"// {line}",
                $"// push {functionName}$ret.{callCount}",
                $"@{functionName}$ret.{callCount}",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push LCL",
                "@LCL",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push ARG",
                "@ARG",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push THIS",
                "@THIS",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push THAT",
                "@THAT",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// reposition ARG",
                "@SP",
                "D=A",
                "@5",
                "D=D-A",
                $"@{nArgs}",
                "D=D-A",
                "@ARG",
                "M=D",
                "// reposition LCL",
                "@SP",
                "D=A",
                "@LCL",
                "M=D",
                $"// goto {functionName}",
                $"@{functionName}",
                "0;JMP",
                $"({functionName}$ret.{callCount})",
                ""
            };
            callCounter.IncrementCount("foo");

            return assembly;
        }
    }
}