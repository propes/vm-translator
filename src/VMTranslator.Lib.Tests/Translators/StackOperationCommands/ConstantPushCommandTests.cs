using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class ConstantPushCommandTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushConstant()
        {
            var command = new Command("push", "constant", "7");
            var expected = new[]
            {
                "@7",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };

            var result = new ConstantPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}