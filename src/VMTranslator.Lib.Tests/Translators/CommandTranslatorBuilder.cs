using Moq;

namespace VMTranslator.Lib.Tests
{
    public partial class CommandTranslatorTests
    {
        private class CommandTranslatorBuilder
        {
            private Mock<ICommandTranslator> mockArithmeticTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockStackOperationTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockLabelTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockGotoTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockIfGotoTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockFunctionTranslator = new Mock<ICommandTranslator>();
            private Mock<ICommandTranslator> mockReturnTranslator = new Mock<ICommandTranslator>();

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

            public CommandTranslatorBuilder WithMockLabelTranslator(
                Mock<ICommandTranslator> mockLabelTranslator)
            {
                this.mockLabelTranslator = mockLabelTranslator;

                return this;
            }

            public CommandTranslatorBuilder WithMockGotoTranslator(
                Mock<ICommandTranslator> mockGotoTranslator)
            {
                this.mockGotoTranslator = mockGotoTranslator;

                return this;
            }

            public CommandTranslatorBuilder WithMockIfGotoTranslator(
                Mock<ICommandTranslator> mockIfGotoTranslator)
            {
                this.mockIfGotoTranslator = mockIfGotoTranslator;

                return this;
            }

            public CommandTranslatorBuilder WithMockFunctionTranslator(
                Mock<ICommandTranslator> mockFunctionTranslator)
            {
                this.mockFunctionTranslator = mockFunctionTranslator;

                return this;
            }

            public CommandTranslatorBuilder WithMockReturnTranslator(
                Mock<ICommandTranslator> mockReturnTranslator)
            {
                this.mockReturnTranslator = mockReturnTranslator;

                return this;
            }

            public CommandTranslator CreateSut()
            {
                return new CommandTranslator(
                    mockArithmeticTranslator.Object,
                    mockStackOperationTranslator.Object,
                    mockLabelTranslator.Object,
                    mockGotoTranslator.Object,
                    mockIfGotoTranslator.Object,
                    mockFunctionTranslator.Object,
                    mockReturnTranslator.Object
                );
            }
        }
    }
}