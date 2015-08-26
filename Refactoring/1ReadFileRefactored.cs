using System;
using System.IO;

namespace Mateer.ReadFile.Refactored
{
    class ReadFile
    {
        static void Main()
        {
            // Read a file.. go towards ETL..Procedural..OO..Functional..funny quotes
            var fileTextLines = File.ReadAllLines(@"..\..\quotes.csv");
            foreach (var line in fileTextLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
