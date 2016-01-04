using System;
using System.Data.SqlClient;
using System.IO;

namespace Mateer.RSharpCommentsAndDeadCode
{
    // This Console App is run from a scheduled task nightly on the App server
    // to import in dumped files which the Oracle team put there
    // It should try each line independently and be resilient if the line fails to parse
    // It should send an email on a line fail, or global exception
    // It should log to a database table
    public class ETL
    {
        public static void ReadFromAFileAndDisplayQuote()
        {
            var fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            foreach (string line in fileTextLines)
            {
                string[] titleAndQuote = line.Split(',');
                string title = titleAndQuote[0];
                Console.WriteLine("title: {0}", title);

                string quote = titleAndQuote[1];
                Console.WriteLine("quote: {0}", quote);

                var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\PresentationRefactoring\Refactoring\Database1.mdf;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Quote", quote);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}

