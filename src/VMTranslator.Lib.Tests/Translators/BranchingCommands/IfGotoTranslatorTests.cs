using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class IfGotoTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenInvalidCommand_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new IfGotoTranslator().ToAssembly("if-goto"));
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
                "@FOO",
                "D;JNE",
                ""
            };

            var actual = new IfGotoTranslator().ToAssembly("if-goto FOO");

            Assert.Equal(expected, actual);
        }
    }
}