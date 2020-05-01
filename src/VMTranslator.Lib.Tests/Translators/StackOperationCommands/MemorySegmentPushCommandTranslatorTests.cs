using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class MemorySegmentPushCommandTranslatorTests
    {
        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPushLocal5(string segment, string code)
        {
            var command = new Command("push", segment, "5");
            var expected = new []
            {
                $"// push {segment} 5",
                $"@{code}",
                "D=M",
                "@5",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = new MemorySegmentPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPushLocal0(string segment, string code)
        {
            var command = new Command("push", segment, "0");
            var expected = new []
            {
                $"// push {segment} 0",
                $"@{code}",
                "D=M",
                "@0",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = new MemorySegmentPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}