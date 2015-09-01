using System;

namespace QuoteImporter
{
    class CompositionRoot
    {
        static void Main()
        {
            IEmailler emailler = new Emailler();
            ILog log = new Logger(emailler);

            try
            {
                var processor = new QuoteProcessor(log);
                processor.RunImporter();
            }
            // If cannot be handled should bubble up to the top and application stop
            catch (Exception ex)
            {
                log.Exception("Global error handler " + ex.Message);
            }
        }
    }

    public class Emailler : IEmailler
    {
        public void SendEmail(string message)
        {
            // Send an email
            Console.WriteLine("Sending email to admin@asd.com: " + message);
        }
    }

    public class Logger : ILog
    {
        private readonly IEmailler emailler;

        public Logger(IEmailler emailler)
        {
            this.emailler = emailler;
        }

        public void Debug(string message)
        {
            // TODOx: Write to logging database table
            Console.WriteLine("Debug: " + message);
        }

        public void Exception(string message)
        {
            // TODOx: Write to logging database table here
            Console.WriteLine("Exception: " + message);
            emailler.SendEmail("Exception: " + message);
        }
    }

    public class Quote
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
