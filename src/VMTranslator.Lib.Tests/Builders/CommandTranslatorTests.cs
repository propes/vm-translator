using System;
using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public partial class CommandTranslatorTests
    {

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        public void ToAssembly_GivenInvalidInput_ThrowsException(string command)
        {
            var sut = new CommandTranslatorBuilder().CreateSut();

            Assert.ThrowsAny<InvalidOperationException>(() => sut.ToAssembly(command));
        }

        [Theory]
        [InlineData("add")]
        [InlineData("sub")]
        [InlineData("neg")]
        [InlineData("eq")]
        [InlineData("gt")]
        [InlineData("lt")]
        [InlineData("and")]
        [InlineData("or")]
        [InlineData("not")]
        public void ToAssembly_GivenArithmeticCommand_ReturnsCorrectOutput(string command)
        {
            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "arith" });

            var actual = new CommandTranslatorBuilder()
                .WithMockArithmeticTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "arith" }, actual);
        }

        [Theory]
        [InlineData("push")]
        [InlineData("pop")]
        public void ToAssembly_GivenStackOperationCommand_ReturnsCorrectOutput(string command)
        {
            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "stack op" });

            var actual = new CommandTranslatorBuilder()
                .WithMockStackOperationTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "stack op" }, actual);
        }

        [Fact]
        public void ToAssembly_GivenLabelCommand_ReturnsCorrectOutput()
        {
            var command = "label FOO";

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "label" });

            var actual = new CommandTranslatorBuilder()
                .WithMockLabelTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "label" }, actual);
        }

        [Fact]
        public void ToAssembly_GivenGotoCommand_ReturnsCorrectOutput()
        {
            var command = "goto FOO";

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "goto" });

            var actual = new CommandTranslatorBuilder()
                .WithMockGotoTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "goto" }, actual);
        }

        [Fact]
        public void ToAssembly_GivenIfGotoCommand_ReturnsCorrectOutput()
        {
            var command = "if-goto FOO";

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "if-goto" });

            var actual = new CommandTranslatorBuilder()
                .WithMockIfGotoTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "if-goto" }, actual);
        }

        [Fact]
        public void ToAssembly_GivenFunctionCommand_ReturnsCorrectOutput()
        {
            var command = "function foo 2";

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "function" });

            var actual = new CommandTranslatorBuilder()
                .WithMockFunctionTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "function" }, actual);
        }

        [Fact]
        public void ToAssembly_GivenReturnCommand_ReturnsCorrectOutput()
        {
            var command = "return";

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "return" });

            var actual = new CommandTranslatorBuilder()
                .WithMockReturnTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "return" }, actual);
        }

        [Fact]
        public void ToAssembly_GivenCallFunctionCommand_ReturnsCorrectOutput()
        {
            var command = "call";

            var mockTranslator = new Mock<ICommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new [] { "call" });

            var actual = new CommandTranslatorBuilder()
                .WithMockCallFunctionTranslator(mockTranslator)
                .CreateSut()
                .ToAssembly(command);

            Assert.Equal(new [] { "call" }, actual);
        }
    }
}