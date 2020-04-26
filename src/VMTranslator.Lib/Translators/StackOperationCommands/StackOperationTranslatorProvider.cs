using System;

namespace VMTranslator.Lib
{
    public class StackOperationTranslatorProvider : IStackOperationTranslatorProvider
    {
        private readonly IStackOperationCommandTranslator memorySegmentPushCommand;
        private readonly IStackOperationCommandTranslator memorySegmentPopCommand;
        private readonly IStackOperationCommandTranslator constantPushCommand;
        private readonly IStackOperationCommandTranslator staticPushCommand;
        private readonly IStackOperationCommandTranslator staticPopCommand;
        private readonly IStackOperationCommandTranslator pointerPushCommand;
        private readonly IStackOperationCommandTranslator pointerPopCommand;
        private readonly IStackOperationCommandTranslator tempPushCommand;
        private readonly IStackOperationCommandTranslator tempPopCommand;

        public StackOperationTranslatorProvider(
            IMemorySegmentPushCommandTranslator memorySegmentPushCommand,
            IMemorySegmentPopCommandTranslator memorySegmentPopCommand,
            IConstantPushCommandTranslator constantPushCommand,
            IStaticPushCommandTranslator staticPushCommand,
            IStaticPopCommandTranslator staticPopCommand,
            IPointerPushCommandTranslator pointerPushCommand,
            IPointerPopCommandTranslator pointerPopCommand,
            ITempPushCommandTranslator tempPushCommand,
            ITempPopCommandTranslator tempPopCommand)
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

        public IStackOperationCommandTranslator GetTranslatorForCommand(Command command)
        {
            switch (command.Segment)
            {
                case "local":
                case "argument":
                case "this":
                case "that":
                    return command.Keyword == "push" ?
                        memorySegmentPushCommand :
                        memorySegmentPopCommand;

                case "constant":
                    if (command.Keyword == "pop")
                        throw new InvalidOperationException("'pop constant' is an invalid command");

                    return constantPushCommand;

                case "static":
                    return command.Keyword == "push" ?
                        staticPushCommand :
                        staticPopCommand;

                case "pointer":
                    return command.Keyword == "push" ?
                        pointerPushCommand :
                        pointerPopCommand;

                case "temp":
                    return command.Keyword == "push" ?
                        tempPushCommand :
                        tempPopCommand;

                default:
                    throw new InvalidOperationException($"keyword '{command.Keyword}' not recognised");
            }
        }
    }
}