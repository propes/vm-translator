using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StackOperationTranslatorTests
    {
        [Fact]
        public void ToAssembly_ReturnsExpected()
        {
            var command = new Command("push", "local", "3");

            var mockParser = new Mock<ICommandParser>();
            mockParser
                .Setup(p => p.Parse("push local 3"))
                .Returns(command);

            var mockTranslator = new Mock<IStackOperationCommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(command))
                .Returns(new string [] { "..." });

            var mockProvider = new Mock<IStackOperationTranslatorProvider>();
            mockProvider
                .Setup(p => p.GetTranslatorForCommand(command))
                .Returns(mockTranslator.Object);

            var sut = new StackOperationTranslator(
                mockParser.Object,
                mockProvider.Object
            );

            var expected = new string [] { "..." };

            var actual = sut.ToAssembly("push local 3");

            Assert.Equal(expected, actual);
        }
    }
}