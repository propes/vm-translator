using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class CommandParser : ICommandParser
    {
        private readonly Dictionary<string, ICommandParser> parsers;

        public CommandParser(
            ICommandParser arithmeticCommandParser,
            ICommandParser stackOperationCommandParser)
        {
            parsers = new Dictionary<string, ICommandParser>
            {
                { "push", stackOperationCommandParser },
                { "pop", stackOperationCommandParser },
                { "add", arithmeticCommandParser },
                { "sub", arithmeticCommandParser },
                { "neg", arithmeticCommandParser },
                { "eq", arithmeticCommandParser },
                { "gt", arithmeticCommandParser },
                { "lt", arithmeticCommandParser },
                { "and", arithmeticCommandParser },
                { "or", arithmeticCommandParser },
                { "not", arithmeticCommandParser }
            };
        }

        public ICommand Parse(string line, string staticVariableName = null)
        {
            var firstToken = line.Split(' ')[0];

            if (!parsers.ContainsKey(firstToken))
            {
                throw new InvalidOperationException($"{firstToken} is not a recognised command");
            }

            return parsers[firstToken].Parse(line, staticVariableName);
        }
    }
}