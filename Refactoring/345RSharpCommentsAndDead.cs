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
                string[] values = line.Split(',');

                string title = values[0];
                Console.WriteLine("title: {0}", title);

                string quote = values[1];
                Console.WriteLine("quote: {0}", quote);

                // TODOx insert into database if doesn't exist already ie title not there
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)", connection))
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

