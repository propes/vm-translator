using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class GotoTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenInvalidCommand_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new GotoTranslator(string.Empty).ToAssembly("goto"));
        }

        [Fact]
        public void ToAssembly_GivenCommand_ReturnsExpected()
        {
            var expected = new []
            {
                "// goto FOO",
                "@foo.FOO",
                "0;JMP",
                ""
            };

            var actual = new GotoTranslator("foo").ToAssembly("goto FOO");

            Assert.Equal(expected, actual);
        }
    }
}