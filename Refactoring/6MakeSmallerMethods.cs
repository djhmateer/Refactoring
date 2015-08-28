using System;
using System.Data.SqlClient;
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
                string[] values = line.Split(',');

                string title = values[0];
                Console.WriteLine("title: " + title);

                var body = values[1];
                Console.WriteLine("body: " + body);

                var path = Environment.CurrentDirectory;
                var appPath = path.Split(new[] {"bin"}, StringSplitOptions.None)[0];
                var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + appPath +
                                       @"Database1.mdf;Integrated Security=True";

                // TODOx: insert into database if doesn't exist already ie title not there
                using (var connection = new SqlConnection(connectionString))
                {
                    using (
                        var cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)", connection)
                        )
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Quote", body);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}