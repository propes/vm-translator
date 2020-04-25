using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class MemorySegmentPopCommandTests
    {
        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPopLocal5(string segment, string code)
        {
            var lines = new [] { $"pop {segment} 5" };
            var expected = new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{code}",
                "A=M",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "M=D"
            };
            var command = new MemorySegmentPopCommand();

            var result = command.ToAssembly(segment, "5");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPopLocal2(string segment, string code)
        {
            var lines = new [] { $"pop {segment} 2" };
            var expected = new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{code}",
                "A=M",
                "A=A+1",
                "A=A+1",
                "M=D"
            };
            var command = new MemorySegmentPopCommand();

            var result = command.ToAssembly(segment, "2");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("local", "LCL")]
        [InlineData("argument", "ARG")]
        [InlineData("this", "THIS")]
        [InlineData("that", "THAT")]
        public void ToAssembly_TranslatesPopLocal0(string segment, string code)
        {
            var lines = new [] { $"pop {segment} 0" };
            var expected = new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{code}",
                "A=M",
                "M=D"
            };
            var command = new MemorySegmentPopCommand();

            var result = command.ToAssembly(segment, "0");

            Assert.Equal(expected, result);
        }
    }
}