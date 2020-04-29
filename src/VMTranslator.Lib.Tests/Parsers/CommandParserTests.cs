using System;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class CommandParserTests
    {        
        [Theory]
        [InlineData("")]
        [InlineData("pop")]
        [InlineData("pop static")]
        public void Parse_GivenInvalidCommand_ThrowsException(string command)
        {
            var sut = new CommandParser();

            Assert.ThrowsAny<InvalidOperationException>(() => sut.Parse(command));
        }

        [Fact]
        public void Parse_GivenValidCommand_CapturesKeyword()
        {
            var actual = new CommandParser().Parse("pop static 2");

            Assert.Equal("pop", actual.Keyword);
        }
        
        [Fact]
        public void Parse_GivenValidCommand_CapturesSegment()
        {
            var actual = new CommandParser().Parse("pop static 2");

            Assert.Equal("static", actual.Segment);
        }

        [Fact]
        public void Parse_GivenValidCommand_CapturesIndex()
        {
            var actual = new CommandParser().Parse("pop static 2");

            Assert.Equal("2", actual.Index);
        }
    }
}