using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class CommandTranslator : ICommandTranslator
    {
        private readonly Dictionary<string, ICommandTranslator> translators;

        public CommandTranslator(
            ICommandTranslator arithmeticCommandTranslator,
            ICommandTranslator stackOperationCommandTranslator,
            ICommandTranslator labelCommandTranslator,
            ICommandTranslator gotoCommandTranslator,
            ICommandTranslator ifGotoCommandTranslator,
            ICommandTranslator functionCommandTranslator,
            ICommandTranslator returnCommandTranslator)
        {
            translators = new Dictionary<string, ICommandTranslator>
            {
                { "push", stackOperationCommandTranslator },
                { "pop", stackOperationCommandTranslator },
                { "add", arithmeticCommandTranslator },
                { "sub", arithmeticCommandTranslator },
                { "neg", arithmeticCommandTranslator },
                { "eq", arithmeticCommandTranslator },
                { "gt", arithmeticCommandTranslator },
                { "lt", arithmeticCommandTranslator },
                { "and", arithmeticCommandTranslator },
                { "or", arithmeticCommandTranslator },
                { "not", arithmeticCommandTranslator },
                { "label", labelCommandTranslator },
                { "goto", gotoCommandTranslator },
                { "if-goto", ifGotoCommandTranslator },
                { "function", functionCommandTranslator },
                { "return", returnCommandTranslator }
            };
        }

        public IEnumerable<string> ToAssembly(string line)
        {
            var firstToken = line.Split(' ')[0];

            if (!translators.ContainsKey(firstToken))
            {
                throw new InvalidOperationException($"{firstToken} is not a recognised command");
            }

            return translators[firstToken].ToAssembly(line);
        }
    }
}