using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

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
            IEnumerable<string> lines = ReadFileListOfLines();
            foreach (var line in lines)
            {
                var quote = ParseLine(line);
                InsertQuoteIntoDatabase(quote);
            }
            log.Debug("RunImport end");
        }

        private IEnumerable<string> ReadFileListOfLines()
        {
            string[] fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            return fileTextLines.ToList();
        }

        public Quote ParseLine(string line)
        {
            string[] values = line.Split(',');

            if (values.Length < 2)
                throw new ApplicationException("Unknown state - too few commas");

            if (values.Length > 2)
                throw new ApplicationException("Unknown state - too many commas");

            string title = values[0];
            log.Debug("title: {0}" + title);

            string body = values[1];
            log.Debug("quote: {0}" + body);

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