using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

// ETL in a procedural way with fairly good naming
namespace Mateer.ETL
{
    class ETL
    {
        static void Main()
        {
            // Extract (from file)
            var fileTextLines = File.ReadAllLines(@"..\..\quotesWithTitles.csv");
            foreach (var line in fileTextLines)
            {
                // Transform
                // parse the csv line into the title and quote
                string[] values = line.Split(',');

                string title = values[0];
                Console.WriteLine("title: {0}", title);

                string quote = values[1];
                Console.WriteLine("quote: {0}", quote);

                // Load
                // insert into a database if doesn't exist already ie title not there
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Dev\Refactoring\Refactoring\Database1.mdf;Integrated Security=True";
                //Data Source = 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Quotes (Title, Text) VALUES (@Title, @Quote)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Quote", quote);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}