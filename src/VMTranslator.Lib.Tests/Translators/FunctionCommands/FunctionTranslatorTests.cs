using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class FunctionTranslatorTests
    {
        [Theory]
        [InlineData("function")]
        [InlineData("function foo")]
        public void ToAssembly_GivenInvalidCommand_ThrowsException(string command)
        {
            Assert.Throws<InvalidOperationException>(() =>
                new FunctionTranslator().ToAssembly(command));
        }

        [Theory]
        [InlineData("bar")]
        [InlineData("baz")]
        public void ToAssembly_GivenZeroVars_ReturnsExpected(string functionName)
        {
            var line = $"function {functionName} 0";
            var expected = new []
            {
                $"// function {functionName} 0",
                $"({functionName})"
            };

            var actual = new FunctionTranslator().ToAssembly(line);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToAssembly_GivenTwoVars_ReturnsExpected()
        {
            var line = $"function bar 2";
            var expected = new []
            {
                "// function bar 2",
                $"(bar)",
                "// push constant 0",
                "@0",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                "",
                "// push constant 0",
                "@0",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var actual = new FunctionTranslator().ToAssembly(line);

            Assert.Equal(expected, actual);
        }
    }
}