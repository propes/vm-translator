namespace VMTranslator.Lib
{
    public interface IStackOperationTranslatorProvider
    {
        IStackOperationCommandTranslator GetTranslatorForCommand(Command command);
    }
}