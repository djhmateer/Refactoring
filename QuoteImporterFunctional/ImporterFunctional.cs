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
            // Log is an Action with a string input
            Action<string> log = s => Log(s);
            // no side effects, so I can use the function whilst it is being composed and not being run
            log("Main start");

            // ReadFileListOfLines is a Function which **Takes an input..why works??** returns a List of strings
            Func<IEnumerable<string>> readFileListOfLines = () => ReadFileListOfLines(log);
            //Func<Action<string>, IEnumerable<string>> readFileListOfLines = x => ReadFileListOfLines(log);

            Func<string, Quote> parseLine = s => ParseLine(s);
            Action<Quote> insertQuoteIntoDatabase = quote => InsertQuoteIntoDatabase(quote);

            // Compose the Functions together (without running them)
            // Passing in a lambda expression (anonymous function) to Action run
            Action run = () => QuoteImporter(log, readFileListOfLines, parseLine, insertQuoteIntoDatabase);
            run();
            log("Main end");
        }

        // 1. Takes an Action with a string input
        // 2. Takes a Function with no input**why does this work??, which returns a list of strings
        // 3. Takes a Function with a string input, which returns a Quote
        // 4. Takes an Action with a Quote input
        //Func<Action<string>, IEnumerable<string>> readFileListOfLines,
        public static void QuoteImporter(Action<string> log,
            Func<IEnumerable<string>> readFileListOfLines,
            Func<string, Quote> parseLine,
            Action<Quote> insertQuoteIntoDatabase)
        {
            log("QuoteImporter Start");
            // don't want to be passing in the log here again???
            //IEnumerable<string> lines = readFileListOfLines(log);
            IEnumerable<string> lines = readFileListOfLines();
            foreach (var line in lines)
            {
                var quote = parseLine(line);
                insertQuoteIntoDatabase(quote);
            }
            log("QuoteImporter End");
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
