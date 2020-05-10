using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class BootstrapCodeTests
    {
        [Fact]
        public void Name()
        {
            var expected = new []
            {
                "// SP=256",
                "@256",
                "D=A",
                "@SP",
                "M=D",
                "",
                $"// call Sys.init 0",
                $"// push Sys.init$ret.0",
                $"@Sys.init$ret.0",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push LCL",
                "@LCL",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push ARG",
                "@ARG",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push THIS",
                "@THIS",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// push THAT",
                "@THAT",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "// ARG = SP-5-nArgs",
                "@SP",
                "D=M",
                "@5",
                "D=D-A",
                $"@0",
                "D=D-A",
                "@ARG",
                "M=D",
                "// LCL = SP",
                "@SP",
                "D=M",
                "@LCL",
                "M=D",
                $"// goto Sys.init",
                $"@Sys.init",
                "0;JMP",
                $"(Sys.init$ret.0)",
                ""
            };

            var actual = new BootstrapCode().ToAssembly();

            Assert.Equal(expected, actual);
        }
    }
}