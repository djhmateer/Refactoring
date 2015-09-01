using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace QuoteImporter
{
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
}