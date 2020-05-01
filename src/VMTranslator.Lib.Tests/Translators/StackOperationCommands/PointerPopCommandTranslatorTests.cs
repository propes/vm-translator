using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class PointerPopCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_TranslatesPopPointer0()
        {
            var command = new Command("pop", "pointer", "0");
            var expected = new []
            {
                "// pop pointer 0",
                "@SP",
                "AM=M-1",
                "D=M",
                "@THIS",
                "M=D",
                ""
            };

            var result = new PointerPopCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_TranslatesPopPointer1()
        {
            var command = new Command("pop", "pointer", "1");
            var expected = new []
            {
                "// pop pointer 1",
                "@SP",
                "AM=M-1",
                "D=M",
                "@THAT",
                "M=D",
                ""
            };

            var result = new PointerPopCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}