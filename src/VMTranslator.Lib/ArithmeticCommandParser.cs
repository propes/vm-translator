using System;

namespace VMTranslator.Lib
{
    public class ArithmeticCommandParser : ICommandParser
    {
        public ICommand Parse(string line, string staticVariableName = null)
        {
            switch (line)
            {
                case "add":
                    return new AddCommand();

                case "sub":
                    return new SubCommand();

                case "neg":
                    return new NegCommand();

                case "eq":
                    return new EqCommand();

                case "gt":
                    return new GtCommand();

                case "lt":
                    return new LtCommand();

                case "and":
                    return new AndCommand();

                case "or":
                    return new OrCommand();

                case "not":
                    return new NotCommand();

                default:
                    throw new InvalidOperationException($"{line} command not recognised");
            }
        }
    }
}