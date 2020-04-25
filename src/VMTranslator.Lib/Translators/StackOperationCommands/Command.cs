namespace VMTranslator.Lib
{
    public class Command
    {
        public Command(string keyword, string segment, string index)
        {
            Keyword = keyword;
            Segment = segment;
            Index = index;
        }

        public string Keyword { get; private set; }
        public string Segment { get; private set; }
        public string Index { get; private set; }
    }
}