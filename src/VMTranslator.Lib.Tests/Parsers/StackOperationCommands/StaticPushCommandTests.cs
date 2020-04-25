using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StaticPushCommandTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushStatic4()
        {
            var test = $"push static 4";
            var lines = new[] { test };
            var expected = new[]
            {
                "@Foo.4",
                "D=M",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1"
            };

            var result = new StaticPushCommand("Foo").ToAssembly("4");

            Assert.Equal(expected, result);
        }
    }
}