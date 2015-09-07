using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace QuoteImporterFunctional
{
    public class ImporterFunctional
    {
        // Functional style of the QuoteImporter app
        private static void Main()
        {
            // Passing in a lambda expression (anonymous function) to Action run
            // Compose the Functions together (without running them)

            // ReadFileListOfLines needs a Log
            Func<IEnumerable<string>> readFileListOfLines = () => ReadFileListOfLines(Log);
            Action run = () => QuoteImporter(Log, readFileListOfLines, ParseLine, InsertQuoteIntoDatabase);
            // Run QuoteImporter
            run();
        }

        // 1. Takes an Action that takes a string
        // 2. Takes a Function which returns a list of strings
        // 3. Takes a Function with an input of string, which returns a Quote
        // 4. Takes an Action which takes a Quote
        public static void QuoteImporter(Action<string> log,
            Func<IEnumerable<string>> readFileListOfLines,
            Func<string, Quote> parseLine,
            Action<Quote> insertQuoteIntoDatabase)
        {
            log("Start");
            IEnumerable<string> lines = readFileListOfLines();
            foreach (var line in lines)
            {
                var quote = parseLine(line);
                insertQuoteIntoDatabase(quote);
            }
            log("End");
        }

        public static IEnumerable<string> ReadFileListOfLines(Action<string> log)
        {
            log("Start ReadFileListOfLines");
            string[] fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            log("End ReadFileListOfLines");
            return fileTextLines.ToList();
        }

        public static Quote ParseLine(string line)
        {
            string[] values = line.Split(',');

            if (values.Length < 2)
                throw new ApplicationException("Unknown state - too few commas");

            if (values.Length > 2)
                throw new ApplicationException("Unknown state - too many commas");

            string title = values[0];
            //log.Debug("title: {0}" + title);

            string body = values[1];
            //log.Debug("quote: {0}" + body);

            var quote = new Quote
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

        public static void Log(string message)
        {
            Console.WriteLine("log: {0}", message);
        }
    }
}
