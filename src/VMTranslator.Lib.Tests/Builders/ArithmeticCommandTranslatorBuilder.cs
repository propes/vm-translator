using Moq;

namespace VMTranslator.Lib.Tests
{
    public class ArithmeticCommandTranslatorBuilder
    {
        private Mock<ICounter> mockEqCounter = new Mock<ICounter>();
        private Mock<ICounter> mockGtCounter = new Mock<ICounter>();
        private Mock<ICounter> mockLtCounter = new Mock<ICounter>();
    
        public ArithmeticCommandTranslator CreateSut()
        {
            return new ArithmeticCommandTranslator(
                mockEqCounter.Object,
                mockGtCounter.Object,
                mockLtCounter.Object);
        }

        public ArithmeticCommandTranslatorBuilder WithEqCount(int count)
        {
            mockEqCounter.Setup(c => c.Count).Returns(count);

            return this;
        }

        public ArithmeticCommandTranslatorBuilder WithGtCount(int count)
        {
            mockGtCounter.Setup(c => c.Count).Returns(count);

            return this;
        }

        public ArithmeticCommandTranslatorBuilder WithLtCount(int count)
        {
            mockLtCounter.Setup(c => c.Count).Returns(count);

            return this;
        }
    }
}