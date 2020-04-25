using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class ArithmeticCommandTranslator : ICommandTranslator
    {
        private readonly Dictionary<string, ICommand> commands =
            new Dictionary<string, ICommand>
        {
            { "add", new AddCommand() },
            { "sub", new SubCommand() },
            { "neg", new NegCommand() },
            { "eq", new EqCommand() },
            { "gt", new GtCommand() },
            { "lt", new LtCommand() },
            { "and", new AndCommand() },
            { "or", new OrCommand() },
            { "not", new NotCommand() }
        };

        public IEnumerable<string> ToAssembly(string line)
        {
            if (!commands.ContainsKey(line))
            {
                throw new InvalidOperationException($"{line} command not recognised");
            }

            return commands[line].ToAssembly();
        }
    }
}