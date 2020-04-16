namespace VMTranslator.Lib
{
    public interface ICommandParser
    {
        ICommand Parse(string line);
    }

    public class CommandParser : ICommandParser
    {
        public ICommand Parse(string line)
        {
            var parts = line.Split(' ');
            var keyword = parts[0];
            var segment = parts[1];
            var index = parts[2];

            ICommand command = null;

            switch (keyword)
            {
                case "push":
                    switch (segment)
                    {
                        case "local":
                        case "argument":
                        case "this":
                        case "that":
                            command = new SegmentPushCommand(segment, index);
                            break;
                    }
                    break;

                default:
                    command = new SegmentPopCommand(segment, index);
                    break;
            }

            return command;
        }
    }
}