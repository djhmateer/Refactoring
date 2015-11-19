using System;
using System.IO;

// Ctrl K D (Edit, Advanced)
// Alt E I O A (Edit, Intellisense..)
namespace Mateer.Reformat
{
    public class Program
    {
        asdf
        public static void Main(string[] args)
        {
            var reading = File.ReadAllLines(@"..\..\quotes.csv");
            for (int index = 0; index < reading.Length; index++)
            {
                var readingaline = reading[index];
                Console.WriteLine(readingaline);
            }
        }
    }
}


