using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StaticPushCommandTranslatorTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushStatic4()
        {
            var command = new Command("push", "static", "4");
            var expected = new[]
            {
                "// push static 4",
                "@Foo.4",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1",
                ""
            };

            var result = new StaticPushCommandTranslator("Foo").ToAssembly(command);

            Assert.Equal(expected, result);
        }
    }
}