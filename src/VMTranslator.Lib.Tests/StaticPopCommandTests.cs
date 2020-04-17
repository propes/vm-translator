using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StaticPopCommandTests
    {
        [Fact]
        public void ToAssembly_TranslatesPushStatic4()
        {
            var test = $"pop static 4";
            var lines = new[] { test };
            var expected = new[]
            {
                "@SP",
                "AM=M-1",
                "D=M",
                "@Foo.4",
                "M=D"
            };

            var result = new StaticPopCommand("Foo", "4").ToAssembly();

            Assert.Equal(expected, result);
        }
    }
}