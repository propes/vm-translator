using System;
using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class StackOperationCommandParser : ICommandParser
    {
        private readonly string staticVariableName;

        public StackOperationCommandParser(
            string staticVariableName)
        {
            this.staticVariableName = staticVariableName;
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
                        new MemorySegmentPushCommand().ToAssembly(segment, index) :
                        new MemorySegmentPopCommand().ToAssembly(segment, index);

                case "constant":
                    if (keyword == "pop")
                        throw new InvalidOperationException("'pop constant' is an invalid command");

                    return new ConstantPushCommand().ToAssembly(index);

                case "static":
                    return keyword == "push" ?
                        new StaticPushCommand(staticVariableName, index).ToAssembly() :
                        new StaticPopCommand(staticVariableName, index).ToAssembly();

                case "pointer":
                    return keyword == "push" ?
                        new PointerPushCommand(index).ToAssembly() :
                        new PointerPopCommand(index).ToAssembly();

                case "temp":
                    return keyword == "push" ?
                        new TempPushCommand(index).ToAssembly() :
                        new TempPopCommand(index).ToAssembly();

                default:
                    throw new InvalidOperationException($"keyword '{keyword}' not recognised");
            }
        }
    }
}