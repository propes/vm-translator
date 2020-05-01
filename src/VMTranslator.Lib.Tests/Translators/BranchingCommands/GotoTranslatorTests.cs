using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class GotoTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenInvalidCommand_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new GotoTranslator().ToAssembly("goto"));
        }

        [Fact]
        public void ToAssembly_GivenCommand_ReturnsExpected()
        {
            var expected = new []
            {
                "// goto FOO",
                "@FOO",
                "0;JMP",
                ""
            };

            var actual = new GotoTranslator().ToAssembly("goto FOO");
        }
    }
}