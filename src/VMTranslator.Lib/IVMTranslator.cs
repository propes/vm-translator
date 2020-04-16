namespace VMTranslator.Lib
{
    public interface IVMTranslator
    {
        string[] TranslateVMcodeToAssembly(string[] lines);
    }
}