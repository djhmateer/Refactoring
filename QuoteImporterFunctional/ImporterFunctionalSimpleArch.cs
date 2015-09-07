using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace QuoteImporterFunctional
{
    public class ImporterFunctionalSimpleArch
    {
        // Functional style of the QuoteImporter app
        private static void Main()
        {
            // Compose the Functions together (without running them)
            Action run = () => QuoteImporter(ReadFileListOfLines, ParseLine, InsertQuoteIntoDatabase);
            run();
        }

        // 1. Takes a Function with no input, which returns a list of strings
        // 2. Takes a Function with a string input, which returns a Quote
        // 3. Takes an Action with a Quote input
        public static void QuoteImporter(
            Func<IEnumerable<string>> readFileListOfLines,
            Func<string, Quote> parseLine,
            Action<Quote> insertQuoteIntoDatabase)
        {
            IEnumerable<string> lines = readFileListOfLines();
            foreach (var line in lines)
            {
                var quote = parseLine(line);
                insertQuoteIntoDatabase(quote);
            }
        }

        // If there is a return type, then must be a Func<input, input..., output>
        public static IEnumerable<string> ReadFileListOfLines()
        {
            return File.ReadAllLines(@"..\..\quotesWithTitles.csv");
        }

        public static Quote ParseLine(string line)
        {
            string[] values = line.Split(',');

            if (values.Length < 2)
                throw new ApplicationException("Unknown state - too few commas");

            if (values.Length > 2)
                throw new ApplicationException("Unknown state - too many commas");

            string title = values[0];
            string body = values[1];

            return new Quote { Title = title, Body = body };
        }

        private static void InsertQuoteIntoDatabase(Quote quote)
        {
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)");
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Title", quote.Title);
                cmd.Parameters.AddWithValue("@Quote", quote.Body);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
