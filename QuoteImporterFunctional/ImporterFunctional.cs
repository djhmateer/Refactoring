using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace QuoteImporterFunctional
{
    public class ImporterFunctional
    {
        // Functional style of the QuoteImporter app, with logging using a decorator
        private static void Main()
        {
            // ReadFileListOfLines is a Function which returns a List of strings
            // passing it a lambda expression (anonymous function) which has a return type of IEnumerable<string>
            Func<IEnumerable<string>> readFileListOfLines =
                () => ReadFileListOfLinesLogger(ReadFileListOfLines, Log);

            //Func<string, Quote> parseLine = s => ParseLine(s);
            Func<string, Quote> parseLine =
                s => ParseLineLogger(s, ParseLine, Log);

            Action<Quote> insertQuoteIntoDatabase = InsertQuoteIntoDatabase;

            // Compose the Functions together (without running them)
            // Passing in a lambda expression (anonymous function) to Action run
            Action quoteImporter = () => QuoteImporter(
                readFileListOfLines, parseLine, insertQuoteIntoDatabase);

            Action run = () => QuoteImporterLogger(
                quoteImporter,
                Log);

            run();
        }

        public static void QuoteImporterLogger(Action quoteImporter, Action<string> log)
        {
            log("Start QuoteImporter");
            quoteImporter();
            log("End QuoteImporter");
        }

        // 1. Takes an Action with a string input
        // 2. Takes a Function with no input**why does this work??, which returns a list of strings
        // 3. Takes a Function with a string input, which returns a Quote
        // 4. Takes an Action with a Quote input
        //Func<Action<string>, IEnumerable<string>> readFileListOfLines,
        public static void QuoteImporter(
            Func<IEnumerable<string>> readFileListOfLines,
            Func<string, Quote> parseLine,
            Action<Quote> insertQuoteIntoDatabase)
        {
            //log("QuoteImporter Start");
            // don't want to be passing in the log here again???
            //IEnumerable<string> lines = readFileListOfLines(log);
            IEnumerable<string> lines = readFileListOfLines();
            foreach (var line in lines)
            {
                var quote = parseLine(line);
                insertQuoteIntoDatabase(quote);
            }
            //log("QuoteImporter End");
        }

        public static IEnumerable<string> ReadFileListOfLinesLogger(Func<IEnumerable<string>> readFileListOfLines, Action<string> log)
        {
            log("Start ReadFileListOfLines");
            var result = readFileListOfLines();
            // Easy way to view contents of IEnumerable to log
            log($"result of ReadFileListOfLines: {string.Join(Environment.NewLine,result.Select(s => s))}");
            log("End ReadFileListOfLines");
            return result;
        }
        public static IEnumerable<string> ReadFileListOfLines()
        {
            string[] fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            return fileTextLines.ToList();
        }

        public static Quote ParseLineLogger(string input, Func<string, Quote> parseLine, Action<string> log)
        {
            log($"Start ParseLine with input {input}");
            Quote result = parseLine(input);
            log($"End ParseLine with ouput {result.Title}, {result.Body}");
            return result;
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

            return new Quote{Title = title,Body = body};
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

        public static void Log(string message)
        {
            Console.WriteLine("log: {0}", message);
        }
    }
}
