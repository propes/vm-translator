using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class LabelTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenInvalidCommand_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new LabelTranslator().ToAssembly($"label"));
        }

        [Theory]
        [InlineData("FOO")]
        [InlineData("BAR")]
        public void ToAssembly_GivenCommand_ReturnsExpected(string label)
        {
            var expected = new [] { $"({label})" };

            var actual = new LabelTranslator().ToAssembly($"label {label}");

            Assert.Equal(expected, actual);
        }
    }
}