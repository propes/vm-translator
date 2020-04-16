namespace VMTranslator.Lib
{
    public class CommandParser : ICommandParser
    {
        public ICommand Parse(string line)
        {
            var parts = line.Split(' ');
            var keyword = parts[0];
            var segment = parts[1];
            var index = parts[2];

            ICommand command = null;

            switch (segment)
            {
                case "local":
                case "argument":
                case "this":
                case "that":
                    command = keyword == "push" ?
                        (ICommand)new SegmentPushCommand(segment, index) :
                        new SegmentPopCommand(segment, index);
                    break;

                case "constant":
                    break;

                case "static":
                    break;

                case "pointer":
                    break;

                case "temp":
                    break;
            }

            return command;
        }
    }
}