using System;
using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class LabelTranslatorBuilder
    {
        private string filename = string.Empty;
        private IFunctionState functionState = new Mock<IFunctionState>().Object;

        public LabelTranslator CreateSut()
        {
            return new LabelTranslator(filename, functionState);
        }

        public LabelTranslatorBuilder WithFilename(string filename)
        {
            this.filename = filename;

            return this;
        }

        public LabelTranslatorBuilder WithFunctionState(IFunctionState functionState)
        {
            this.functionState = functionState;

            return this;
        }
    }

    public class LabelTranslatorTests
    {
        [Fact]
        public void ToAssembly_GivenInvalidCommand_ThrowsException()
        {
            var sut = new LabelTranslatorBuilder().CreateSut();

            Assert.Throws<InvalidOperationException>(() => sut.ToAssembly($"label"));
        }

        [Theory]
        [InlineData("FOO")]
        [InlineData("BAR")]
        public void ToAssembly_GivenCommandInFunction_ReturnsExpected(string label)
        {
            var expected = new [] { $"(foo.bar${label})" };

            var functionState = new FunctionState
            {
                CurrentFunction = "bar"
            };

            var sut = new LabelTranslatorBuilder()
                .WithFilename("foo")
                .WithFunctionState(functionState)
                .CreateSut();

            var actual = sut.ToAssembly($"label {label}");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FOO")]
        [InlineData("BAR")]
        public void ToAssembly_GivenCommandOutOfFunction_ReturnsExpected(string label)
        {
            var expected = new [] { $"(foo.{label})" };

            var functionState = new FunctionState
            {
                CurrentFunction = "bar"
            };

            var sut = new LabelTranslatorBuilder()
                .WithFilename("foo")
                .CreateSut();

            var actual = sut.ToAssembly($"label {label}");

            Assert.Equal(expected, actual);
        }
    }
}