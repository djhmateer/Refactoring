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
        // Firstly decorate each separate function (1,2,3) with it's logger
        // Then compose together, decorating that top level function too
        private static void Main()
        {
            // 1. ReadFileListOfLines is a function with 0 Parameters, which returns a List of strings
            // passing it a lambda expression (anonymous function)
            // Log takes 1 parameter s
            // ReadFileListOfLines takes 0 parameters
            // returns a string
            Func<IEnumerable<string>> readFileListOfLines =
                () => ReadFileListOfLinesLogger(() => ReadFileListOfLines(), s => Log(s));

            // 2. ParseLine takes 1 Parameter string s, which is wired up to lineToParse
            // using method group - don't need to specify s => Log(s)
            // returns a Quote
            Func<string, Quote> parseLine = 
                lineToParse => ParseLineLogger(s => ParseLine(s), Log, lineToParse);

            // 3. InsertQuoteIntoDatabase takes 1 parameter Quote
            // returns nothing (therefore its an Action)
            // quote is wired up to q in the logger
            Action<Quote> insertQuoteIntoDatabase =
                quote => InsertQuoteIntoDatabaseLogger(q => InsertQuoteIntoDatabase(q), Log, quote);

            // Compose the Functions together (without running them)
            // Passing in a lambda expression (anonymous function) to Action run
            Action quoteImporter = 
                () => QuoteImporter(readFileListOfLines, parseLine, insertQuoteIntoDatabase);

            // Decorating logging in the top level function (but no logging in this composition root function)
            Action run = () => QuoteImporterLogger(quoteImporter,Log);
            run();
        }

        public static void QuoteImporterLogger(Action quoteImporter, Action<string> log)
        {
            log("Start QuoteImporter");
            quoteImporter();
            log("End QuoteImporter");
        }

        // Top level function, which depends upon 3 other functions (passed in as parameters)
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

        // 1 ReadFile List
        public static IEnumerable<string> ReadFileListOfLinesLogger(
            Func<IEnumerable<string>> readFileListOfLines, Action<string> log)
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

        // 2 ParseLine
        public static Quote ParseLineLogger(
            Func<string, Quote> parseLine, Action<string> log, string lineToParse)
        {
            log($"Start ParseLine with input {lineToParse}");
            Quote result = parseLine(lineToParse);
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

        // 3 InsertQuote
        public static void InsertQuoteIntoDatabaseLogger(
            Action<Quote> insertQuoteIntoDatabase,Action<string> log, Quote quote)
        {
            log($"Start InsertQuoteIntoDatabase with input {quote.Title} {quote.Body}");
            insertQuoteIntoDatabase(quote);
            log($"End InsertQuoteIntoDatabase");
        }

        private static void InsertQuoteIntoDatabase(Quote quote)
        {
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)", connection);
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
