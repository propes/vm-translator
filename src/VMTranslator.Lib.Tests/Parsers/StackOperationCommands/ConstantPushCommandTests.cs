using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class ConstantPushCommandTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushConstant()
        {
            var test = $"push constant 7";
            var lines = new[] { test };
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
            var command = new ConstantPushCommandTranslator();

            var result = command.ToAssembly("7");

            Assert.Equal(expected, result);
        }
    }
}