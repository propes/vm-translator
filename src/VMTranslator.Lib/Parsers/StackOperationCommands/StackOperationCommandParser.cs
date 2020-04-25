using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StackOperationCommandParser : ICommandParser
    {
        private readonly IMemorySegmentCommand memorySegmentPushCommand;
        private readonly IMemorySegmentCommand memorySegmentPopCommand;
        private readonly IConstantCommand constantPushCommand;
        private readonly IStaticCommand staticPushCommand;
        private readonly IStaticCommand staticPopCommand;
        private readonly IPointerCommand pointerPushCommand;
        private readonly IPointerCommand pointerPopCommand;
        private readonly ITempCommand tempPushCommand;
        private readonly ITempCommand tempPopCommand;
        private readonly string staticVariableName;

        public StackOperationCommandParser(
            IMemorySegmentCommand memorySegmentPushCommand,
            IMemorySegmentCommand memorySegmentPopCommand,
            IConstantCommand constantPushCommand,
            IStaticCommand staticPushCommand,
            IStaticCommand StaticPopCommand,
            IPointerCommand pointerPushCommand,
            IPointerCommand pointerPopCommand,
            ITempCommand tempPushCommand,
            ITempCommand TempPopCommand)
        {
            this.memorySegmentPushCommand = memorySegmentPushCommand;
            this.memorySegmentPopCommand = memorySegmentPopCommand;
            this.constantPushCommand = constantPushCommand;
            this.staticPushCommand = staticPushCommand;
            staticPopCommand = StaticPopCommand;
            this.pointerPushCommand = pointerPushCommand;
            this.pointerPopCommand = pointerPopCommand;
            this.tempPushCommand = tempPushCommand;
            tempPopCommand = TempPopCommand;
        }

        public IEnumerable<string> Parse(string line)
        {
            var parts = line.Split(' ');
            var keyword = parts[0];
            var segment = parts[1];
            var index = parts[2];

            switch (segment)
            {
                case "local":
                case "argument":
                case "this":
                case "that":
                    return keyword == "push" ?
                        memorySegmentPushCommand.ToAssembly(segment, index) :
                        memorySegmentPopCommand.ToAssembly(segment, index);

                case "constant":
                    if (keyword == "pop")
                        throw new InvalidOperationException("'pop constant' is an invalid command");

                    return constantPushCommand.ToAssembly(index);

                case "static":
                    return keyword == "push" ?
                        staticPushCommand.ToAssembly(index) :
                        staticPopCommand.ToAssembly(index);

                case "pointer":
                    return keyword == "push" ?
                        pointerPushCommand.ToAssembly(index) :
                        pointerPopCommand.ToAssembly(index);

                case "temp":
                    return keyword == "push" ?
                        tempPushCommand.ToAssembly(index) :
                        tempPopCommand.ToAssembly(index);

                default:
                    throw new InvalidOperationException($"keyword '{keyword}' not recognised");
            }
        }
    }
}