using System.Collections.Generic;

namespace VMTranslator.Lib
{

    public class StackOperationTranslator : ICommandTranslator
    {
        private readonly ICommandParser commandParser;
        private readonly IStackOperationTranslatorProvider translatorProvider;

        public StackOperationTranslator(
            ICommandParser commandParser,
            IStackOperationTranslatorProvider translatorProvider)
        {
            this.commandParser = commandParser;
            this.translatorProvider = translatorProvider;
        }

        public IEnumerable<string> ToAssembly(string line)
        {
            var command = commandParser.Parse(line);

            var translator = translatorProvider.GetTranslatorForCommand(command);

            return translator.ToAssembly(command);
        }
    }
}