namespace VMTranslator.Lib
{
    public class CommandParser : ICommandParser
    {
        public Command Parse(string line)
        {
            var parts = line.Split(' ');

            return new Command(parts[0], parts[1], parts[2]);
        }
    }
}