namespace VMTranslator
{
    public interface IFunctionCallCounter
    {
        int GetCount(string functionName);

        void IncrementCount(string functionName);
    }
}