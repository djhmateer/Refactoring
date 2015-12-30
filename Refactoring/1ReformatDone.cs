using System;
using System.IO;

namespace Mateer.Reformat.Refactored
{
    class ReadFile
    {
        static void Main()
        {
            var fileTextLines = File.ReadAllLines(@"..\..\quotes.csv");
            foreach (var line in fileTextLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("Done!");
        }
    }
}

// Read a file.. go towards ETL..Procedural..OO..Functional..funny quotes
