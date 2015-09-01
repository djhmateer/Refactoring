using System;

namespace QuoteImporter
{
    public class FakeLogger : ILog
    {
        private readonly IEmailler emailler;

        public FakeLogger(IEmailler emailler)
        {
            this.emailler = emailler;
        }

        public void Debug(string message)
        {
            Console.WriteLine("FakeLogger Debug: " + message);
        }

        public void Exception(string message)
        {
            Console.WriteLine("FakeLogger Exception: " + message);
        }
    }
}