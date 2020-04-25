using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StackOperationCommandTranslator : ICommandTranslator
    {
        private readonly ICommandParser commandParser;
        private readonly IStackOperationCommandTranslator memorySegmentPushCommand;
        private readonly IStackOperationCommandTranslator memorySegmentPopCommand;
        private readonly IStackOperationCommandTranslator constantPushCommand;
        private readonly IStackOperationCommandTranslator staticPushCommand;
        private readonly IStackOperationCommandTranslator staticPopCommand;
        private readonly IStackOperationCommandTranslator pointerPushCommand;
        private readonly IStackOperationCommandTranslator pointerPopCommand;
        private readonly IStackOperationCommandTranslator tempPushCommand;
        private readonly IStackOperationCommandTranslator tempPopCommand;

        public StackOperationCommandTranslator(
            ICommandParser commandParser,
            IStackOperationCommandTranslator memorySegmentPushCommand,
            IStackOperationCommandTranslator memorySegmentPopCommand,
            IStackOperationCommandTranslator constantPushCommand,
            IStackOperationCommandTranslator staticPushCommand,
            IStackOperationCommandTranslator staticPopCommand,
            IStackOperationCommandTranslator pointerPushCommand,
            IStackOperationCommandTranslator pointerPopCommand,
            IStackOperationCommandTranslator tempPushCommand,
            IStackOperationCommandTranslator tempPopCommand)
        {
            this.commandParser = commandParser;
            this.memorySegmentPushCommand = memorySegmentPushCommand;
            this.memorySegmentPopCommand = memorySegmentPopCommand;
            this.constantPushCommand = constantPushCommand;
            this.staticPushCommand = staticPushCommand;
            this.staticPopCommand = staticPopCommand;
            this.pointerPushCommand = pointerPushCommand;
            this.pointerPopCommand = pointerPopCommand;
            this.tempPushCommand = tempPushCommand;
            this.tempPopCommand = tempPopCommand;
        }

        public IEnumerable<string> ToAssembly(string line)
        {
            var command = commandParser.Parse(line);

            switch (command.Segment)
            {
                case "local":
                case "argument":
                case "this":
                case "that":
                    return command.Keyword == "push" ?
                        memorySegmentPushCommand.ToAssembly(command) :
                        memorySegmentPopCommand.ToAssembly(command);

                case "constant":
                    if (command.Keyword == "pop")
                        throw new InvalidOperationException("'pop constant' is an invalid command");

                    return constantPushCommand.ToAssembly(command);

                case "static":
                    return command.Keyword == "push" ?
                        staticPushCommand.ToAssembly(command) :
                        staticPopCommand.ToAssembly(command);

                case "pointer":
                    return command.Keyword == "push" ?
                        pointerPushCommand.ToAssembly(command) :
                        pointerPopCommand.ToAssembly(command);

                case "temp":
                    return command.Keyword == "push" ?
                        tempPushCommand.ToAssembly(command) :
                        tempPopCommand.ToAssembly(command);

                default:
                    throw new InvalidOperationException($"keyword '{command.Keyword}' not recognised");
            }
        }
    }
}