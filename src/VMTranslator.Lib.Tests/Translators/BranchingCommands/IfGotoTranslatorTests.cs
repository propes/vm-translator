using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class IfGotoTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenInvalidCommand_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new IfGotoTranslator(string.Empty).ToAssembly("if-goto"));
        }

        [Fact]
        public void ToAssembly_GivenCommand_ReturnsExpected()
        {
            var expected = new []
            {
                "// if-goto FOO",
                "@SP",
                "AM=M-1",
                "D=M",
                "@foo.FOO",
                "D;JNE",
                ""
            };

            var actual = new IfGotoTranslator("foo").ToAssembly("if-goto FOO");

            Assert.Equal(expected, actual);
        }
    }
}