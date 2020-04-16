using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class TextCleanerTests
    {
        private TextCleaner CreateSut()
        {
            return new TextCleaner();
        }

        [Theory]
        [InlineData("// remove this", "")]
        [InlineData("keep this // remove this", "keep this ")]
        [InlineData("keep this", "keep this")]
        public void StripComments_RemovesComments(string value, string expected)
        {
            var result = CreateSut().StripComments(value);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("foo", "foo")]
        [InlineData(" foo ", "foo")]
        [InlineData("foo bar", "foo bar")]
        [InlineData("foo  bar", "foo bar")]
        [InlineData("   ", "")]
        [InlineData("\t", "")]
        [InlineData("\n", "")]
        public void StripWhitespace_RemovesWhitespace(string value, string expected)
        {
            var result = CreateSut().StripWhitespace(value);

            Assert.Equal(expected, result);
        }
    }
}
