namespace VMTranslator.Lib
{
    public class TextCleaner : ITextCleaner
    {
        public string StripComments(string text)
        {
            var i = text.IndexOf("//");
            return i < 0 ? text : text.Remove(i);
        }
        public string StripWhitespace(string text)
        {
            return text.Trim()
                .Replace("  ", " ")
                .Replace("\t", "")
                .Replace("\n", "");
        }
    }
}