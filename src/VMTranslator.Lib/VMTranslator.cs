using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class VMTranslator : IVMTranslator
    {
        private readonly ITextCleaner textCleaner;
        private readonly ICommandParser commandParser;

        public VMTranslator(ITextCleaner textCleaner, ICommandParser commandParser)
        {
            this.textCleaner = textCleaner;
            this.commandParser = commandParser;
        }

        public string[] TranslateVMcodeToAssembly(string[] lines)
        {
            var translatedLines = new List<string>(lines.Length);

            foreach (var line in lines)
            {
                var cleanLines = textCleaner.StripComments(line);
                cleanLines = textCleaner.StripWhitespace(cleanLines);

                if (string.IsNullOrEmpty(cleanLines))
                    continue;

                translatedLines.Add("// " + cleanLines);
                translatedLines.AddRange(commandParser.Parse(cleanLines));
                translatedLines.Add("");
            }

            return translatedLines.ToArray();
        }
    }
}