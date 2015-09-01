using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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
                var service = new QuoteProcessor(log);
                service.RunImporter();
            }
            // If cannot be handled should bubble up to the top and application stop
            catch (Exception ex)
            {
                log.Exception("Global error handler " + ex.Message);
            }
        }
    }

    public class QuoteProcessor
    {
        private readonly ILog log;

        public QuoteProcessor(ILog log)
        {
            this.log = log;
        }

        public void RunImporter()
        {
            log.Debug("RunImport start");
            var fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            foreach (var line in fileTextLines)
            {
                var quote = ParseLine(line);
                InsertQuoteIntoDatabase(quote);
            }
            log.Debug("RunImport end");
        }

        public Quote ParseLine(string line)
        {
            string[] values = line.Split(',');

            if (values.Length < 2)
                throw new ApplicationException("Unknown state - too few commas");

            if (values.Length > 2)
                throw new ApplicationException("Unknown state - too many commas");

            string title = values[0];
            Console.WriteLine("title: {0}", title);

            string body = values[1];
            Console.WriteLine("quote: {0}", body);

            var quote = new Quote
            {
                Title = title,
                Body = body
            };

            return quote;
        }

        private void InsertQuoteIntoDatabase(Quote quote)
        {
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Title", quote.Title);
                cmd.Parameters.AddWithValue("@Quote", quote.Body);
                connection.Open();
                cmd.ExecuteNonQuery();
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
        private readonly IEmailler _emailler;

        public Logger(IEmailler emailler)
        {
            _emailler = emailler;
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
            _emailler.SendEmail("Exception: " + message);
        }
    }

    

    public class Quote
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
