using System;
using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StackOperationTranslatorProviderTests
    {
        private StackOperationTranslatorProvider CreateSut()
        {
            return new StackOperationTranslatorProvider(
                new Mock<IMemorySegmentPushCommandTranslator>().Object,
                new Mock<IMemorySegmentPopCommandTranslator>().Object,
                new Mock<IConstantPushCommandTranslator>().Object,
                new Mock<IStaticPushCommandTranslator>().Object,
                new Mock<IStaticPopCommandTranslator>().Object,
                new Mock<IPointerPushCommandTranslator>().Object,
                new Mock<IPointerPopCommandTranslator>().Object,
                new Mock<ITempPushCommandTranslator>().Object,
                new Mock<ITempPopCommandTranslator>().Object
            );
        }

        [Theory]
        [InlineData("push", "local", typeof(IMemorySegmentPushCommandTranslator))]
        [InlineData("push", "argument", typeof(IMemorySegmentPushCommandTranslator))]
        [InlineData("push", "this", typeof(IMemorySegmentPushCommandTranslator))]
        [InlineData("push", "that", typeof(IMemorySegmentPushCommandTranslator))]
        [InlineData("pop", "local", typeof(IMemorySegmentPopCommandTranslator))]
        [InlineData("pop", "argument", typeof(IMemorySegmentPopCommandTranslator))]
        [InlineData("pop", "this", typeof(IMemorySegmentPopCommandTranslator))]
        [InlineData("pop", "that", typeof(IMemorySegmentPopCommandTranslator))]
        [InlineData("push", "constant", typeof(IConstantPushCommandTranslator))]
        [InlineData("push", "static", typeof(IStaticPushCommandTranslator))]
        [InlineData("pop", "static", typeof(IStaticPopCommandTranslator))]
        [InlineData("push", "pointer", typeof(IPointerPushCommandTranslator))]
        [InlineData("pop", "pointer", typeof(IPointerPopCommandTranslator))]
        [InlineData("push", "temp", typeof(ITempPushCommandTranslator))]
        [InlineData("pop", "temp", typeof(ITempPopCommandTranslator))]
        public void GetTranslatorForCommand_ReturnsCorrectType(
            string operation,
            string segment,
            Type type)
        {
            var command = new Command(operation, segment, string.Empty);

            var result = CreateSut().GetTranslatorForCommand(command);

            Assert.IsAssignableFrom(type, result);
        }
    }
}