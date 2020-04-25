namespace VMTranslator.Lib
{
    public class Counter : ICounter
    {
        private int count = 0;

        public int Count => count;

        public void Increment()
        {
            count++;
        }
    }
}