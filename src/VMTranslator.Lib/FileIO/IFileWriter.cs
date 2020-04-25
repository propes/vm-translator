namespace VMTranslator.Lib
{
    public interface IFileWriter
    {
        void WriteArrayToFile(string filename, string[] translatedLines);
    }
}