using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class TempPopCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_TranslatesPopTemp5()
        {
            var command = new Command("pop", "temp", "5");
            var expected = new []
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@R5",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "A=A+1",
                "M=D"
            };

            var result = new TempPopCommandTranslator().ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}