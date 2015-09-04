using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace QuoteImporterFunctional
{
    public static class ImporterFunctional
    {
        // Functional style of the QuoteImporter app
        private static void Main()
        {
            Action run = Run;
            run();
        }

        private static void Run()
        {
            // pass in the 'functions' to RunQuoteImporter that are its dependencies

            // what is ReadFileListOfLines needs the log?
            RunQuoteImporter(Log, ReadFileListOfLines, ParseLine, InsertQuoteIntoDatabase);
        }

        public static void RunQuoteImporter(Action<string> log,
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

        public static IEnumerable<string> ReadFileListOfLines()
        {
            string[] fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
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