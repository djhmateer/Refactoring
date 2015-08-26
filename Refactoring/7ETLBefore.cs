using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Mateer.ETL
{
    class ETL
    {
        static void Main()
        {
            var fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            foreach (var line in fileTextLines)
            {
                string[] values = line.Split(',');

                string title = values[0];
                Console.WriteLine("title: {0}", title);

                string quote = values[1];
                Console.WriteLine("quote: {0}", quote);

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
    }
}