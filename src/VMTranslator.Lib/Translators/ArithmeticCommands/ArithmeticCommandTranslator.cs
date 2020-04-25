using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class ArithmeticCommandTranslator : ICommandTranslator
    {
        private readonly Dictionary<string, ICommand> commands;

        public ArithmeticCommandTranslator(
            ICounter eqCommandCounter,
            ICounter gtCommandCounter,
            ICounter ltCommandCounter)
        {
            commands = new Dictionary<string, ICommand>
            {
                { "add", new AddCommand() },
                { "sub", new SubCommand() },
                { "neg", new NegCommand() },
                { "eq", new EqCommand(eqCommandCounter) },
                { "gt", new GtCommand(gtCommandCounter) },
                { "lt", new LtCommand(ltCommandCounter) },
                { "and", new AndCommand() },
                { "or", new OrCommand() },
                { "not", new NotCommand() }
            };
        }

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