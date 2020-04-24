using System;

namespace VMTranslator.Lib
{
    public class StackOperationCommandParser : ICommandParser
    {
        public ICommand Parse(string line, string staticVariableName = null)
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
                    if (keyword == "pop")
                        throw new InvalidOperationException("'pop constant' is an invalid command");

                    command = new ConstantPushCommand(index);
                    break;

                case "static":
                    command = keyword == "push" ?
                        (ICommand)new StaticPushCommand(staticVariableName, index) :
                        new StaticPopCommand(staticVariableName, index);
                    break;

                case "pointer":
                    command = keyword == "push" ?
                        (ICommand)new PointerPushCommand(index) :
                        new PointerPopCommand(index);
                    break;

                case "temp":
                    command = keyword == "push" ?
                        (ICommand)new TempPushCommand(index) :
                        new TempPopCommand(index);
                    break;
            }

            return command;
        }
    }
}