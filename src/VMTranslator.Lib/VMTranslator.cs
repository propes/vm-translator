using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class VMTranslator : IVMTranslator
    {
        private readonly ITextCleaner textCleaner;
        private readonly ICommandParser commandParser;
        private readonly string staticVariableName;

        public VMTranslator(
            ITextCleaner textCleaner,
            ICommandParser commandParser,
            string staticVariableName)
        {
            this.textCleaner = textCleaner;
            this.commandParser = commandParser;
            this.staticVariableName = staticVariableName;
        }

        public string[] TranslateVMcodeToAssembly(string[] lines)
        {
            var translatedLines = new List<string>(lines.Length);

            foreach (var line in lines)
            {
                var clean = textCleaner.StripComments(line);
                clean = textCleaner.StripWhitespace(clean);

                if (string.IsNullOrEmpty(clean))
                    continue;

                translatedLines.Add("// " + clean);
                var command = commandParser.Parse(clean, staticVariableName);
                translatedLines.AddRange(command.ToAssembly());
                translatedLines.Add("");
            }

            return translatedLines.ToArray();
        }
    }
}