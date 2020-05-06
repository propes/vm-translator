using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class FunctionCallCounter : IFunctionCallCounter
    {
        private readonly Dictionary<string, int> callCount = new Dictionary<string, int>();

        public int GetCount(string functionName)
        {
            return callCount.ContainsKey(functionName) ? callCount[functionName] : 0;
        }

        public void IncrementCount(string functionName)
        {
            if (!callCount.ContainsKey(functionName))
            {
                callCount.Add(functionName, 0);
            }

            callCount[functionName]++;
        }
    }
}