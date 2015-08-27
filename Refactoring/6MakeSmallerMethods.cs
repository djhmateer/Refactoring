using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
                // Extract method, ParseLine
                string quote;
                var title = ParseLine(line, out quote);

                // TODOx: insert into database if doesn't exist already ie title not there
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
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

        //**HERE**
        private static string ParseLine(string line, out string body)
        {
            string[] values = line.Split(',');

            string title = values[0];
            Debug.WriteLine("title: " + title);

            body = values[1];
            Debug.WriteLine("body: " + body);
            return title;
        }
    }

    public class Quote
    {
        public String Title { get; set; }
        public String Body { get; set; }
    }
}