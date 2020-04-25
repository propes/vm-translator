using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StackOperationCommandTranslator : ICommandTranslator
    {
        private readonly ICommandParser commandParser;
        private readonly IMemorySegmentCommandTranslator memorySegmentPushCommand;
        private readonly IMemorySegmentCommandTranslator memorySegmentPopCommand;
        private readonly IConstantCommandTranslator constantPushCommand;
        private readonly IStaticCommandTranslator staticPushCommand;
        private readonly IStaticCommandTranslator staticPopCommand;
        private readonly IPointerCommandTranslator pointerPushCommand;
        private readonly IPointerCommandTranslator pointerPopCommand;
        private readonly ITempCommandTranslator tempPushCommand;
        private readonly ITempCommandTranslator tempPopCommand;

        public StackOperationCommandTranslator(
            ICommandParser commandParser,
            IMemorySegmentCommandTranslator memorySegmentPushCommand,
            IMemorySegmentCommandTranslator memorySegmentPopCommand,
            IConstantCommandTranslator constantPushCommand,
            IStaticCommandTranslator staticPushCommand,
            IStaticCommandTranslator staticPopCommand,
            IPointerCommandTranslator pointerPushCommand,
            IPointerCommandTranslator pointerPopCommand,
            ITempCommandTranslator tempPushCommand,
            ITempCommandTranslator tempPopCommand)
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