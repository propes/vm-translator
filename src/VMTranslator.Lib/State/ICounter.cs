namespace VMTranslator.Lib
{
    public interface ICounter
    {
        int Count { get; }

        void Increment();
    }
}