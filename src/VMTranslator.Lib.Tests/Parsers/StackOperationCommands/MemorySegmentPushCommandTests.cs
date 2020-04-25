using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class MemorySegmentPushCommandTests
    {
        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPushLocal5(string segment, string code)
        {
            var expected = new []
            {
                $"@{code}",
                "D=M",
                "@5",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
            var command = new MemorySegmentPushCommandTranslator();

            var result = command.ToAssembly(segment, "5");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPushLocal0(string segment, string code)
        {
            var expected = new []
            {
                $"@{code}",
                "D=M",
                "@0",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };
            var command = new MemorySegmentPushCommandTranslator();

            var result = command.ToAssembly(segment, "0");

            Assert.Equal(expected, result);
        }
    }
}