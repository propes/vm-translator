using System;

namespace VMTranslator.Lib
{
    public class CommandParser : ICommandParser
    {
        public Command Parse(string line)
        {
            var parts = line.Split(' ');

            if (parts.Length < 3)
            {
                throw new InvalidOperationException("Command must be in the format 'keyword segment index'");
            }

            return new Command(parts[0], parts[1], parts[2]);
        }
    }
}