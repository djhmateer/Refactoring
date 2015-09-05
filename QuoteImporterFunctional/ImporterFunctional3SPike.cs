using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;

namespace QuoteImporterFunctional
{
    public static class ImporterFunctional3Spike
    {
        // Functional style of the QuoteImporter app
        private static void Main()
        {
            // 2 levels - works
            //Action run = () => RunFileReader(Log);
            Action runFileReader = () => RunFileReader(Log);
            // 3 levels - compiles
            Action run = () => ThingThatNeedsRunFileReader(runFileReader);
            run();
        }

        public static void ThingThatNeedsRunFileReader(Action runFileReader)
        {
            // want to do all business logic here..
            runFileReader();
        }

        // Passing in a log function with no return (Action) which has a string parameter
        public static void RunFileReader(Action<string> log)
        {
            Console.WriteLine("RunFileReader");
            log("RunFileReader");
        }

        public static void Log(string message)
        {
            Console.WriteLine("log: {0}", message);
        }


        //public static void QuoteImporter(Action<string> log,
        //    Func<Action<string>,IEnumerable<string>> readFileListOfLines,
        //    Func<string, Quote> parseLine,
        //    Action<Quote> insertQuoteIntoDatabase)
        //{
        //    log("Start");
        //    IEnumerable<string> lines = readFileListOfLines();
        //    foreach (var line in lines)
        //    {
        //        var quote = parseLine(line);
        //        insertQuoteIntoDatabase(quote);
        //    }
        //    log("End");
        //}

        public static IEnumerable<string> ReadFileListOfLines(Action<string> log)
        {
            log("Start read");
            string[] fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            log("End read");
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
    }
}