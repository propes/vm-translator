using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class PointerPushCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushPointer0()
        {
            var command = new Command("push", "pointer", "0");
            var expected = new []
            {
                "@THIS",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };

            var result = new PointerPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToAssembly_TranslatesPushPointer1()
        {
            var command = new Command("push", "pointer", "1");
            var expected = new []
            {
                "@THAT",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };

            var result = new PointerPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}