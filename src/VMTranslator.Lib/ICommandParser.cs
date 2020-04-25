namespace VMTranslator.Lib
{
    public interface ICommandParser
    {
        ICommand Parse(string line);
    }
}