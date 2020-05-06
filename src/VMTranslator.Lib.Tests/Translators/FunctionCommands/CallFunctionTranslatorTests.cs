using System;
using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class CallFunctionTranslatorTests
    {
        private CallFunctionTranslator CreateSut()
        {
            return new CallFunctionTranslator(new Mock<IFunctionCallCounter>().Object);
        }

        private CallFunctionTranslator CreateSutWithCallCounter(IFunctionCallCounter callCounter)
        {
            return new CallFunctionTranslator(callCounter);
        }

        [Theory]
        [InlineData("call")]
        [InlineData("call function")]
        public void ToAssembly_GivenInvalidCommand_ThrowsException(string command)
        {
            Assert.Throws<InvalidOperationException>(() =>
                CreateSut().ToAssembly(command));
        }

        [Theory]
        [InlineData("foo", 2)]
        [InlineData("bar", 3)]
        public void ToAssembly_GivenCommand_ReturnsExpected(string functionName, int nArgs)
        {
            var line = $"call {functionName} {nArgs}";
            var expected = new []
            {
                $"// call {functionName} {nArgs}",
                $"// push {functionName}$ret.0",
                $"@{functionName}$ret.0",
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
                $"@{nArgs}",
                "D=D-A",
                "@ARG",
                "M=D",
                "// LCL = SP",
                "@SP",
                "D=M",
                "@LCL",
                "M=D",
                $"// goto {functionName}",
                $"@{functionName}",
                "0;JMP",
                $"({functionName}$ret.0)",
                ""
            };

            var actual = CreateSut().ToAssembly(line);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToAssembly_GivenRepeatCalls_IncrementsReturnLabel()
        {
            var callCounter = new FunctionCallCounter();

            CreateSutWithCallCounter(callCounter).ToAssembly("call foo 0");
            CreateSutWithCallCounter(callCounter).ToAssembly("call foo 0");
            var actual = CreateSutWithCallCounter(callCounter).ToAssembly("call foo 0");

            Assert.Contains("// push foo$ret.2", actual);
            Assert.Contains("@foo$ret.2", actual);
            Assert.Contains("(foo$ret.2)", actual);
        }
    }
}