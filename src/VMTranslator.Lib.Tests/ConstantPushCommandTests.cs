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
            var command = new ConstantPushCommand("7");

            var result = command.ToAssembly();

            Assert.Equal(expected, result);
        }
    }
}