using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StaticPopCommandTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushStatic4()
        {
            var command = new Command("push", "static", "4");
            var expected = new[]
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@Foo.4",
                "M=D"
            };

            var result = new StaticPopCommandTranslator("Foo").ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}