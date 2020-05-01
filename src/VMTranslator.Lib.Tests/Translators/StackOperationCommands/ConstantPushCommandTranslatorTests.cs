using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class ConstantPushCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushConstant()
        {
            var command = new Command("push", "constant", "7");
            var expected = new[]
            {
                "// push constant 7",
                "@7",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = new ConstantPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}