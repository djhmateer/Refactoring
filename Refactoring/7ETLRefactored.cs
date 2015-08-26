using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Mateer.ETL.Refactored
{
    public class Quote
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }

    class ETL
    {
        static void Main()
        {
            // Extract (from file)
            var fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            foreach (var line in fileTextLines)
            {
                // Transform
                // parse the csv line into the title and quote
                var quote = ParseLine(line);

                // Load
                // insert into a database if doesn't exist already ie title not there
                InsertQuoteIntoDatabase(quote);
            }
        }

        private static Quote ParseLine(string line)
        {
            string[] values = line.Split(',');

            string title = values[0];
            Console.WriteLine("title: {0}", title);

            string body = values[1];
            Console.WriteLine("quote: {0}", body);

            Quote quote = new Quote
            {
                Title = title,
                Body = body
            };

            return quote;
        }

        private static void InsertQuoteIntoDatabase(Quote quote)
        {
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)");
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
