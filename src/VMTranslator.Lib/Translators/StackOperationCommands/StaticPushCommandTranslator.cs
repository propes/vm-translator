using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StaticPushCommandTranslator : IStaticCommandTranslator
    {
        private readonly string variableName;

        public StaticPushCommandTranslator(string variableName)
        {
            this.variableName = variableName;
        }

        public IEnumerable<string> ToAssembly(Command command)
        {
            return new[]
            {
                $"@{variableName}.{command.Index}",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
            };
        }
    }
}