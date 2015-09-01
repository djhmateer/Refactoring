namespace QuoteImporter
{
    public interface IEmailler
    {
        void SendEmail(string body);
    }
}