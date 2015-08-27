using System;

namespace QuoteImporter
{
    public class FakeEmailler : IEmailler
    {
        public void SendEmail(string body)
        {
            Console.WriteLine("FakeEmailler: " + body);
        }
    }
}