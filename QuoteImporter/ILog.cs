namespace QuoteImporter
{
    public interface ILog
    {
        void Debug(string message);
        void Exception(string message);
    }
}