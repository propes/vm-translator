namespace VMTranslator.Lib
{
    public interface ITextCleaner
    {
        string StripComments(string text);
        string StripWhitespace(string text);
    }
}