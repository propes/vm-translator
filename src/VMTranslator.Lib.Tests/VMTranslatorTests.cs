using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class VMTranslatorTests
    {
        private VMTranslator CreateSut()
        {
            return new VMTranslator(
                new TextCleaner(),
                new Mock<ICommandTranslator>().Object
            );
        }

        private VMTranslator CreateSutWithTranslator(ICommandTranslator translator)
        {
            return new VMTranslator(
                new TextCleaner(),
                translator
            );
        }

        [Fact]
        public void TranslateVMcodeToAssembly_IgnoresWhitespace()
        {
            var lines = new string[]
            {
                "  ",
                "\n",
                "\t"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Empty(result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_IgnoresComments()
        {
            var lines = new []
            {
                "// Ignore this"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Empty(result);
        }

        [Fact]
        public void TranslateVMcodeToAssembly_CopiesOriginalCodeAsComment()
        {
            var lines = new []
            {
                "push local 5"
            };

            var result = CreateSut().TranslateVMcodeToAssembly(lines);

            Assert.Equal("// push local 5", result[0]);
        }

        [Theory]
        [InlineData("push local 0")]
        [InlineData("push temp 1")]
        public void TranslateVMcodeToAssembly_ReturnsExpected(string line)
        {
            var lines = new [] { line };
            var expected = new []
            {
                $"// {line}",
                "...",
                ""
            };

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(line))
                .Returns(new string [] { "..." });

            var result = CreateSutWithTranslator(mockTranslator.Object)
                .TranslateVMcodeToAssembly(lines);

            Assert.Equal(expected, result);
        }
    }
}
