using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class CommandParser : ICommandParser
    {
        private readonly ICommandParser arithmeticCommandParser;
        private readonly ICommandParser stackOperationCommandParser;

        private readonly Dictionary<string, CommandType> commandTypes =
            new Dictionary<string, CommandType>
            {
                { "push", CommandType.StackOperation },
                { "pop", CommandType.StackOperation },
                { "add", CommandType.Arithmetic },
                { "sub", CommandType.Arithmetic },
                { "neg", CommandType.Arithmetic },
                { "eq", CommandType.Arithmetic },
                { "gt", CommandType.Arithmetic },
                { "lt", CommandType.Arithmetic },
                { "and", CommandType.Arithmetic },
                { "or", CommandType.Arithmetic },
                { "not", CommandType.Arithmetic }
            };

        public CommandParser(
            ICommandParser arithmeticCommandParser,
            ICommandParser stackOperationCommandParser)
        {
            this.arithmeticCommandParser = arithmeticCommandParser;
            this.stackOperationCommandParser = stackOperationCommandParser;
        }

        public ICommand Parse(string line, string staticVariableName = null)
        {
            var firstToken = line.Split(' ')[0];

            if (!commandTypes.ContainsKey(firstToken))
            {
                throw new InvalidOperationException($"{firstToken} is not a recognised command");
            }

            if (commandTypes[firstToken] == CommandType.Arithmetic)
                return arithmeticCommandParser.Parse(line, staticVariableName);

            return stackOperationCommandParser.Parse(line, staticVariableName);
        }
    }
}