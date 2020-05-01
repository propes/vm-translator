using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class TempPushCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushTemp3()
        {
            var command = new Command("push", "temp", "3");
            var expected = new []
            {
                "// push temp 3",
                "@R5",
                "D=A",
                "@3",
                "A=D+A",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = new TempPushCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}