using Moq;
using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class StackOperationProviderTranslatorTests
    {
        private StackOperationTranslatorProvider CreateSut()
        {
            return new StackOperationTranslatorProvider(
                new MemorySegmentPushCommandTranslator(),
                new MemorySegmentPopCommandTranslator(),
                new ConstantPushCommandTranslator(),
                new StaticPushCommandTranslator(string.Empty),
                new StaticPopCommandTranslator(string.Empty),
                new PointerPushCommandTranslator(),
                new PointerPopCommandTranslator(),
                new TempPushCommandTranslator(),
                new TempPopCommandTranslator()
            );
        }

        [Fact]
        public void Test()
        {
            CreateSut();
        }
    }
}