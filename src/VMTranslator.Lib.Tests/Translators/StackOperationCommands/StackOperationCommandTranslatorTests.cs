using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StackOperationCommandTranslatorBuilder
    {
            private Mock<IStackOperationCommandTranslator> mockMemorySegmentPushCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockMemorySegmentPopCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockConstantPushCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockStaticPushCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockStaticPopCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockTempPushCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockTempPopCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockPointerPushCommand = new Mock<IStackOperationCommandTranslator>();
            private Mock<IStackOperationCommandTranslator> mockPointerPopCommand = new Mock<IStackOperationCommandTranslator>();

        public StackOperationTranslator CreateSut()
        {
            return new StackOperationTranslator(
                new CommandParser(),
                new StackOperationTranslatorProvider(
                    mockMemorySegmentPushCommand.Object,
                    mockMemorySegmentPopCommand.Object,
                    mockConstantPushCommand.Object,
                    mockStaticPushCommand.Object,
                    mockStaticPopCommand.Object,
                    mockTempPushCommand.Object,
                    mockTempPopCommand.Object,
                    mockPointerPushCommand.Object,
                    mockPointerPopCommand.Object
                )
            );
        }

        public StackOperationCommandTranslatorBuilder WithMockTranslator(
            string commandType,
            Mock<IStackOperationCommandTranslator> mockTranslator)
        {
            switch (commandType)
            {
                case "memorySegmentPush":
                    mockMemorySegmentPushCommand = mockTranslator;
                    break;

                case "memorySegmentPop":
                    mockMemorySegmentPopCommand = mockTranslator;
                    break;

                case "constantPush":
                    mockConstantPushCommand = mockTranslator;
                    break;

                case "staticPush":
                    mockStaticPushCommand = mockTranslator;
                    break;

                case "staticPop":
                    mockStaticPopCommand = mockTranslator;
                    break;

                case "tempPush":
                    mockTempPushCommand = mockTranslator;
                    break;

                case "tempPop":
                    mockTempPopCommand = mockTranslator;
                    break;

                case "pointerPush":
                    mockPointerPushCommand = mockTranslator;
                    break;

                case "pointerPop":
                    mockPointerPopCommand = mockTranslator;
                    break;
            }

            return this;
        }
    }

    public class StackOperationCommandTranslatorTests
    {
        [Theory]
        [InlineData("push local 0", "memorySegmentPush")]
        [InlineData("push local 1", "memorySegmentPush")]
        [InlineData("pop local 0", "memorySegmentPop")]
        public void ToAssembly_ReturnsExpected(string line, string commandType)
        {
            var parts = line.Split(' ');
            var mockTranslator = new Mock<IStackOperationCommandTranslator>();
            mockTranslator
                .Setup(t => t.ToAssembly(It.Is<Command>(
                    c => c.Keyword == parts[0] &&
                         c.Segment == parts[1] &&
                         c.Index == parts[2]
                )))
                .Returns(new string [] { "..."});

            var expected = new string []
            {
                "..."
            };

            var actual = new StackOperationCommandTranslatorBuilder()
                .WithMockTranslator(commandType, mockTranslator)
                .CreateSut()
                .ToAssembly(line);

            Assert.Equal(expected, actual);
        }
    }
}