using System;
using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class CommandTranslatorTests
    {
        private class CommandTranslatorBuilder
        {
            private Mock<ICommandTranslator> mockArithmeticTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockStackOperationTranslator = new Mock<ICommandTranslator>();

            public CommandTranslatorBuilder WithMockArithmeticTranslator(
                Mock<ICommandTranslator> mockArithmeticTranslator)
            {
                this.mockArithmeticTranslator = mockArithmeticTranslator;

                return this;
            }

            public CommandTranslatorBuilder WithMockStackOperationTranslator(
                Mock<ICommandTranslator> mockStackOperationTranslator)
            {
                this.mockStackOperationTranslator = mockStackOperationTranslator;

                return this;
            }

            public CommandTranslator CreateSut()
            {
                return new CommandTranslator(
                    mockArithmeticTranslator.Object,
                    mockStackOperationTranslator.Object
                );
            }
        }

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
    }
}