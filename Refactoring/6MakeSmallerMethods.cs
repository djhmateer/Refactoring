using System;
using System.Data.SqlClient;
using System.IO;

namespace Mateer.MakeSmallerMethods
{
    public class ETL
    {
        public static void ReadFromAFileAndDisplayQuote()
        {
            var fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            foreach (var line in fileTextLines)
            {
                // comment... to method name..extrcted!
                Quote quote = ParseLine(line);
                SaveToDatabase(quote);
            }
        }

        private static void SaveToDatabase(Quote quote)
        {
            var connectionString = GetConnectionString();

            // TODOx: insert into database if doesn't exist already ie title not there
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)", connection))
                {
                    cmd.Parameters.AddWithValue("@Title", quote.Title);
                    cmd.Parameters.AddWithValue("@Quote", quote.Body);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static string GetConnectionString()
        {
            var path = Environment.CurrentDirectory;
            var appPath = path.Split(new[] { "bin" }, StringSplitOptions.None)[0];
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + appPath +
                                   @"Database1.mdf;Integrated Security=True";
            return connectionString;
        }

        private static Quote ParseLine(string line)
        {
            string[] values = line.Split(',');

            string title = values[0];
            Console.WriteLine("title: " + title);
            var quote = new Quote { Title = title };

            var body = values[1];
            Console.WriteLine("body: " + body);
            quote.Body = body;
            return quote;
        }
    }
    public class Quote
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}