using Xunit;

namespace VMTranslator.Lib.Tests
{
    public class CounterTests
    {
        [Fact]
        public void Count_InitialCountIsZero()
        {
            var actual = new Counter().Count;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void Increment_IncrementsCountToOne()
        {
            var counter = new Counter();
            counter.Increment();

            Assert.Equal(1, counter.Count);
        }

        [Fact]
        public void Increment_IncrementsCountToTwo()
        {
            var counter = new Counter();
            counter.Increment();
            counter.Increment();

            Assert.Equal(2, counter.Count);
        }
    }
}