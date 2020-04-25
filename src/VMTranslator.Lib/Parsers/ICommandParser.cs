namespace VMTranslator.Lib
{
    public interface ICommandParser
    {
        Command Parse(string line);
    }
}