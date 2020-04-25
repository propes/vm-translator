using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StackOperationCommandTranslator : ICommandTranslator
    {
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