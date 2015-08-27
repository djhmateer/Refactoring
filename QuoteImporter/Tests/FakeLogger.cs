using System;

namespace QuoteImporter
{
    public class FakeLogger : ILog
    {
        private readonly IEmailler _emailler;

        public FakeLogger(IEmailler emailler)
        {
            _emailler = emailler;
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