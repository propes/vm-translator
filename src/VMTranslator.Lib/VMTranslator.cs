using System.Collections.Generic;

namespace VMTranslator.Lib
{
    public class VMTranslator : IVMTranslator
    {
        private readonly ITextCleaner textCleaner;
        private readonly ICommandTranslator translator;

        public VMTranslator(ITextCleaner textCleaner, ICommandTranslator translator)
        {
            this.textCleaner = textCleaner;
            this.translator = translator;
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
                translatedLines.AddRange(translator.ToAssembly(cleanLines));
                translatedLines.Add("");
            }

            return translatedLines.ToArray();
        }
    }
}